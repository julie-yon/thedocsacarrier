using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Threading.Tasks;

namespace Utility
{
    public class LogWriter
    {
        public string Path = @"Assets/Scripts/Twitch/Log";
        public string Extention = ".txt";
        static StreamWriter S_writer = null;

        public LogWriter()
        {
            string totalPath = Path + Extention;

            try
            {

                if (!File.Exists(totalPath)) 
                {
                    File.Create(totalPath);
                } 
                S_writer = new StreamWriter(totalPath, true);
                S_writer.AutoFlush = true;

                string recordingStartMessage = $"Recod start {DateTime.Now}.";

                Write(recordingStartMessage, true);
                NewLine();
                Write("Recorded Contents : ", true);
            }
            catch (Exception e)
            {
                MonoBehaviour.print(e);
            }
        }

        public LogWriter(string path)
        {
            // Path = path;  
            // string totalPath = Path + Extention;

            // if (!File.Exists(totalPath)) 
            // {
            //     File.Create(totalPath);
            //     S_writer = new StreamWriter(totalPath);
            //     Write("This File is made at " + DateTime.Now, true);
            // } else
            // {
            //     S_writer = new StreamWriter(totalPath, true);
            // }
        }

        ~LogWriter()
        {
            string destructorMessage = $"Destructor has been called at {DateTime.Now}";
            
            NewLine();
            Write(destructorMessage, true);
            Write("================================================", true);
            S_writer.Flush();
            S_writer.Close();
        }

        public void Write(string content, bool newLine=false)
        {
            WriteAfterFileExist(content);
            if (newLine) NewLine();
        }

        private async void WriteAfterFileExist(string content)
        {
            if (!File.Exists(Path + Extention))
            {
                await Task.Yield();
            }

            S_writer.Write(content);
        }

        public void NewLine()
        {
            WriteAfterFileExist("\n");
        }

        public void Flush()
        {
            S_writer.Flush();
        }

        public void Close()
        {
            string closingMessage = $"LogWriter has been Closed at {DateTime.Now}";
            
            NewLine();
            Write(closingMessage, true);
            Write("================================================", true);
            S_writer.Close();
        }

        public static void DebugWrite(string content)
        {
            S_writer.Write("Debug msg : " + content);
            S_writer.WriteLine();
        }
    }
}