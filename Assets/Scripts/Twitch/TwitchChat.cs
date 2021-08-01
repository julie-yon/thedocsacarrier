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

        void Awake()
        {
            DontDestroyObjects.Add(this);
        }

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

        public void Connect()
        {
            DontDestroyObjects.Add(this);
            
            _twitchClient = new TcpClient("irc.chat.twitch.tv", PortNumber);
            _twitchReader = new StreamReader(_twitchClient.GetStream());
            _twitchWriter = new StreamWriter(_twitchClient.GetStream());
            
            _twitchWriter.WriteLine("PASS " + OAuthAuthorization);
            _twitchWriter.WriteLine("NICK " + IRCHostName);
            _twitchWriter.WriteLine("USER " + IRCHostName + " 8 * :" + IRCHostName);
            _twitchWriter.WriteLine("JOIN #" + ChannelName);
            _twitchWriter.Flush();
        }

        public void ConnectCoroutine()
        {
            Coroutine coroutine;

            coroutine = StartCoroutine(ConnectMethod());
        }

        IEnumerator ConnectMethod()
        {
            StartUIManager.instance.Checker = true;

            _twitchClient = new TcpClient("irc.chat.twitch.tv", PortNumber);
            _twitchReader = new StreamReader(_twitchClient.GetStream());
            _twitchWriter = new StreamWriter(_twitchClient.GetStream());
            
            _twitchWriter.WriteLine("PASS " + OAuthAuthorization);
            _twitchWriter.WriteLine("NICK " + IRCHostName);
            _twitchWriter.WriteLine("USER " + IRCHostName + " 8 * :" + IRCHostName);
            _twitchWriter.WriteLine("JOIN #" + ChannelName);
            _twitchWriter.Flush();
            
            yield return new WaitForSeconds(CheckingConnectivityTimeLimit);

            Connected = _twitchClient.Connected;
            print("Connected : " + Connected);

            if (Connected)
            {
                Core.instance.GameStart();
            } else
            {
                print("Failed to connect to Twtich Chnnel.");
                LogWriter.Instance.Write("Failed to connect to Twtich Chnnel.", true);
            }
        }

        public async Task ConnectAsync()
        {
            await Task.Run(() => {
            StartUIManager.instance.Checker = true;
            DontDestroyObjects.Add(this);

            _twitchClient = new TcpClient("irc.chat.twitch.tv", PortNumber);
            _twitchReader = new StreamReader(_twitchClient.GetStream());
            _twitchWriter = new StreamWriter(_twitchClient.GetStream());
            
            _twitchWriter.WriteLine("PASS " + OAuthAuthorization);
            _twitchWriter.WriteLine("NICK " + IRCHostName);
            _twitchWriter.WriteLine("USER " + IRCHostName + " 8 * :" + IRCHostName);
            _twitchWriter.WriteLine("JOIN #" + ChannelName);
            _twitchWriter.Flush();
            
            CheckConnectivity();

            if (Connected)
            {
                Core.instance.GameStart();
            } else
            {
                print("Failed to connect to Twtich Chnnel.");
                LogWriter.Instance.Write("Failed to connect to Twtich Chnnel.", true);
            }
            });
        }
        
        private void CheckConnectivity()
        {
            Task.Delay((int)CheckingConnectivityTimeLimit * 1000).Wait();

            Connected = _twitchClient.Connected;
            print("Connected : " + Connected);
        }

        void ReadChat()
        {
            if (_twitchClient.Available > 0)
            {
                string msg = _twitchReader.ReadLine();
                print(msg);
                
#if UNITY_EDITOR
                LogWriter.Instance.Write(msg, true);
#endif

                if (msg.Contains("PING"))
                {
                    _twitchWriter.WriteLine("PONG");
                    _twitchWriter.Flush();
                    return;
                }

                if (msg.Contains("PRIVMSG")){
                    var splitPoint = msg.IndexOf("!", 1);
                    print(splitPoint);
                    var author = msg.Substring(0, splitPoint);
                    print(author);
                    author = author.Substring(1);
                    print(author);

                    // users msg
                    splitPoint = msg.IndexOf(":", 1);
                    print(splitPoint);
                    msg = msg.Substring(splitPoint + 1);

                    print(msg);
                    if(msg.StartsWith(TwitchCommandData.Prefix)){
                        // get the first word 
                        string command = msg.Substring(1);
                        DocsaTwitchCommand commandEnum = StringValue.GetEnumValue<DocsaTwitchCommand>(command);
                        DocsaSakkiManager.instance.ExecuteCommand(
                            new TwitchCommandData {
                                Author = author,
                                Command = commandEnum,
                                Time = System.DateTime.Now,
                        });
                    }
                }            
            }
        }
    }
}