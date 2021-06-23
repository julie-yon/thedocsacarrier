using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using UnityEngine;
using Utility;

namespace TwitchIRC
{
    public class TwitchChat : MonoBehaviour
    {
        [Header("Twitch IRC Settings")]
        [Tooltip("You have to get OAuth from 'https://twitchapps.com/tmi/'")]
        public string OAuthAuthorization;
        [Tooltip("Twitch ID which you used to get OAuth")]
        public string IRCHostName;
        [Tooltip("Twitch channel name that you want get chats")]
        public string ChannelName;

        [Space(15)]
        TcpClient _twitchClient;
        StreamReader _twitchReader;
        StreamWriter _twitchWriter;
        bool _connected;

#if UNITY_EDITOR
    LogWriter _logWriter;
#endif
        void Awake()
        {
            _logWriter = new LogWriter();
            Connect();
        }

        void OnDestroy()
        {
            _twitchReader.Close();
            _twitchWriter.Close();
            _logWriter.Close();
        }

        void Update()
        {
            if (_twitchClient != null && _twitchClient.Connected)
            {
                ReadChat();
            }
        }

        void Connect()
        {
            _twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
            _twitchReader = new StreamReader(_twitchClient.GetStream());
            _twitchWriter = new StreamWriter(_twitchClient.GetStream());
            
            _twitchWriter.WriteLine("PASS " + OAuthAuthorization);
            _twitchWriter.WriteLine("NICK " + IRCHostName);
            _twitchWriter.WriteLine("USER " + IRCHostName + " 8 * :" + IRCHostName);
            _twitchWriter.WriteLine("JOIN #" + ChannelName);
            _twitchWriter.Flush();
            
            DontDestroyObjects.Add(this);
        }

        void ReadChat()
        {
            if (_twitchClient.Available > 0)
            {
                string msg = _twitchReader.ReadLine();

#if UNITY_EDITOR
                _logWriter.Write(msg, true);
#endif

                if (msg.Contains("PING"))
                {
                    _twitchWriter.WriteLine("PONG");
                    _twitchWriter.Flush();
                    return;
                }
                print(msg);
            }
        }
    }
}