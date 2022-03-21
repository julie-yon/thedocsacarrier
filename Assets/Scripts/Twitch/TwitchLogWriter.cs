using System.IO;
using System;
using UnityEngine;
using System.Threading.Tasks;

namespace Utility
{
    public class TwitchLogWriter : LogWriter
    {
        static LogWriter _instance;
        public static LogWriter Instance {
            get {
                if (_instance == null)
                {
                    _instance = new TwitchLogWriter();
                }

                return _instance;
            }
        }
        public string Path = @"Assets/Scripts/Twitch/Log";
        public string Extention = ".txt";
        StreamWriter writer = null;

        public TwitchLogWriter() : base(TwitchLogWriter.Instance.Path, TwitchLogWriter.Instance.Extention)
        {
        }

        ~TwitchLogWriter()
        {
            string destructorMessage = $"Destructor has been called at {DateTime.Now}";
            
            NewLine();
            Write(destructorMessage, true);
            Write("================================================", true);
            writer.Flush();
            writer.Close();
        }

        private async void WriteAfterFileExist(string content)
        {
            if (!File.Exists(Path + Extention))
            {
                await Task.Yield();
            }

            writer.Write(content);
        }

        public override void Close()
        {
            string closingMessage = $"LogWriter has been Closed at {DateTime.Now}";
            
            NewLine();
            Write(closingMessage, true);
            Write("================================================", true);
            base.Close();
        }
    }
}