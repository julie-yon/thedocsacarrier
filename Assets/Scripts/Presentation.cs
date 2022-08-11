using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net.Sockets;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

using Docsa;
using Docsa.Character;
using dkstlzu.Utility;
using TwitchIRC;
using TMPro;

namespace Docsa.Presentation
{
    public class Presentation : MonoBehaviour
    {
        public DocsaSakki DocsaCharater1;
        public DocsaSakki DocsaCharater2;
        public DocsaData DocsaData1;
        public DocsaData DocsaData2;

        public TwitchCommandData[] commandDatas;
        void OnGUI()
        {
            if (GUI.Button(new Rect(50, 50, 200, 50), "Connect"))
            {
                TwitchChat.instance.Connect();
            }

            if (GUI.Button(new Rect(300, 50, 200, 50), "Assign"))
            {
                Assign();
            }

            if (GUI.Button(new Rect(550, 50, 200, 50), "Do"))
            {
                foreach (var v in commandDatas)
                {
                    DocsaSakkiManager.instance.ExecuteCommand(v);
                }
            }

            if (Keyboard.current.uKey.wasPressedThisFrame)
            {
                // DocsaSakkiManager.instance.
            }
        }

        void Assign()
        {
            DocsaData1 = DocsaSakkiManager.instance.AttendingDocsaDict["dkstlzu"];
            DocsaData2 = DocsaSakkiManager.instance.AttendingDocsaDict["dev_test_dkstlzu2"];
            DocsaData1.Character = DocsaCharater1;
            DocsaData2.Character = DocsaCharater2;
        }
    }
}