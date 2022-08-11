using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using dkstlzu.Utility;

public class StreamWriterTest : MonoBehaviour
{
    public string DataPath;
    public string ConsoleLogPath;
    public string StreamingAssetPath;
    public string TwitchPath = "/Scripts/Twitch";
    public string FileName = "/Somefile.txt";
    public bool rawUpdate;
    public bool nullCheck;
    StreamWriter _writer = null;

    public LogWriter _logWriter;
 
    void Awake()
    {
        DataPath = Application.dataPath;
        ConsoleLogPath = Application.consoleLogPath;
        StreamingAssetPath = Application.streamingAssetsPath;
        TwitchPath = Application.dataPath + TwitchPath;

        _logWriter = new LogWriter();
    }
    
    void Start()
    {
        File.Create(DataPath + FileName);
        // File.Create(ConsoleLogPath + FileName); // GetError
        File.Create(StreamingAssetPath + FileName);
        File.Create(TwitchPath + FileName);
    }

    void Update()
    {
        // 가장 문제없어 보이는 1번방법
        if (_writer == null && nullCheck)
        {
            _writer = new StreamWriter(StreamingAssetPath + FileName, true);
            _writer.AutoFlush = true;
        }

        // 처음부분 2개의 줄이 시간적으로 이격되서 찍히는 2번방법
        if (Input.GetKey(KeyCode.P))
        {
            _writer = new StreamWriter(StreamingAssetPath + FileName, true);
            _writer.AutoFlush = true;
        }

        // 한번만 찍히는 이상한 3번방법
        if (rawUpdate)
        {
            _writer = new StreamWriter(StreamingAssetPath + FileName, true);
            _writer.AutoFlush = true;
        }

        if (_writer != null)
            _writer.Write("Time " + Time.time + " has passed\n");
    }

    void OnDestroy()
    {
        _writer.Close();
        _logWriter.Close();
    }
 
    int index = 0;
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Write"))
        {
            index++;
            using(StreamWriter writer = new StreamWriter(StreamingAssetPath + FileName, true))
            {
                writer.Write("Why Error Please " + index + " At : " + Time.time);
                writer.WriteLine();
            }
        }

        if (GUI.Button(new Rect(120, 10, 100, 30), "LogWriter"))
        {
            _logWriter.Write("sdfsdf", true);
            _logWriter.Flush();
        }
    }

}
