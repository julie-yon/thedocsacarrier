using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using UnityEngine;

using Utility;
using Docsa;

namespace TwitchIRC
{
    public class TwitchChat : Singleton<TwitchChat>
    {
        [Header("Twitch IRC Settings")]
        [Tooltip("You have to get OAuth from 'https://twitchapps.com/tmi/'")]
        public string OAuthAuthorization;
        [Tooltip("Twitch ID which you used to get OAuth")]
        public string IRCHostName;
        [Tooltip("Twitch channel name that you want get chats")]
        public string ChannelName;
        public int PortNumber = 6667;

        [Space(15)]
        TcpClient _twitchClient;
        StreamReader _twitchReader;
        StreamWriter _twitchWriter;
        public float CheckingConnectivityTimeLimit = 5;
        public bool Connected;

        void OnDestroy()
        {
            if (Connected)
            {
                _twitchReader.Close();
                _twitchWriter.Close();
            }
        }

        void Update()
        {
            if (_twitchClient != null && _twitchClient.Connected)
            {
                ReadChat();
            }
        }

        public void Init()
        {
            _twitchClient = new TcpClient("irc.chat.twitch.tv", PortNumber);
            _twitchReader = new StreamReader(_twitchClient.GetStream());
            _twitchWriter = new StreamWriter(_twitchClient.GetStream());
        }

        public void Connect()
        {
            _twitchWriter.WriteLine("PASS " + OAuthAuthorization);
            _twitchWriter.WriteLine("NICK " + IRCHostName);
            _twitchWriter.WriteLine("USER " + IRCHostName + " 8 * :" + IRCHostName);
            _twitchWriter.WriteLine("JOIN #" + ChannelName);
            _twitchWriter.Flush();
        }

        public async void ConnectAsync()
        {
            Init();
            
            float connectStartTime = Time.time;
            do
            {
                if (Time.time - connectStartTime > CheckingConnectivityTimeLimit) break;

                Connect();
                await Task.Delay(1000);
            } while (!Connected);
            
            if (Connected)
            {
                Core.instance.GameStart();
            } else
            {
                print("Failed to connect to Twtich Chnnel.");
            }
        }

        void ReadChat()
        {
            if (_twitchClient.Available > 0)
            {
                string msg = _twitchReader.ReadLine();
                if (msg.Length > 0) Connected = true;
                print(msg);

                if (msg.Contains("PING"))
                {
                    _twitchWriter.WriteLine("PONG");
                    _twitchWriter.Flush();
                    return;
                }

                if (msg.Contains("PRIVMSG")){
                    var splitPoint = msg.IndexOf("!", 1);
                    var author = msg.Substring(0, splitPoint);
                    author = author.Substring(1);

                    // users msg
                    splitPoint = msg.IndexOf(":", 1);
                    msg = msg.Substring(splitPoint + 1);

                    print(msg);
                    if(msg.StartsWith(TwitchCommandData.Prefix)){
                        // get the first word 
                        splitPoint = msg.IndexOf(" ", 1);
                        string command = string.Empty;
                        string chat = string.Empty;
                        if (splitPoint > 0)
                        {
                            command = msg.Substring(1, splitPoint-1);
                            chat = msg.Substring(splitPoint+1);
                        }
                        else
                            command = msg.Substring(1);

                        print("Command : " + command);
                        print("Chat : " + chat);
                        DocsaTwitchCommand commandEnum = StringValue.GetEnumValue<DocsaTwitchCommand>(command);
                        print("CommandEnum : " + commandEnum.ToString());
                        DocsaSakkiManager.instance.ExecuteCommand(
                            new TwitchCommandData {
                                Author = author,
                                Command = commandEnum,
                                Time = System.DateTime.Now,
                                Chat = chat,
                        });
                    }
                }            
            }
        }
    }
}