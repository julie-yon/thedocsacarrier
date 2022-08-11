using System.IO;
using System;
using UnityEngine;
using System.Threading.Tasks;

namespace dkstlzu.Utility
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

        public TwitchLogWriter() : base(@"Logs/", "TwitchLog", "txt")
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