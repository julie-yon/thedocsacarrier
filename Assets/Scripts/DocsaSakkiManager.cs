using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchIRC;

namespace Docsa
{
    public class DocsaSakkiManager : Singleton<DocsaSakkiManager>
    {
        List<string> AttendingDocsas;
        Dictionary<string, Character.Docsa> DocsaDict;
        Dictionary<string, Character.Hunter> HunterDict;

        public GameObject StarLightGameObject;
        
        void Awake()
        {
            AttendingDocsas = new List<string>();
            DocsaDict = new Dictionary<string, Character.Docsa>();
            HunterDict = new Dictionary<string, Character.Hunter>();
        }

        public void ExecuteCommand(TwitchCommandData commandData)
        {
            switch (commandData.Command)
            {
                case DocsaTwitchCommand.NONE:
                break;

                case DocsaTwitchCommand.ATTEND:
                Attend(commandData);
                break;
                case DocsaTwitchCommand.EXIT:
                Exit(commandData);
                break;
                case DocsaTwitchCommand.STARLIGHT:
                StarLight();
                break;

                case DocsaTwitchCommand.DOCSA_ATTACK:
                DocsaChim(commandData);
                break;
                case DocsaTwitchCommand.DOCSA_JUMP:
                DocsaJump(commandData);
                break;
                case DocsaTwitchCommand.HUNTER_NET:
                HunterNet(commandData);
                break;
                case DocsaTwitchCommand.HUNTER_ATTACK:
                HunterAttack(commandData);
                break;
            }
        }

        void Attend(TwitchCommandData commandData)
        {
            if (AttendingDocsas.Contains(commandData.Author))
            {
                return;
            }
        }

        void Exit(TwitchCommandData commandData)
        {
            if (AttendingDocsas.Contains(commandData.Author))
            {
                return;
            }
        }

        void StarLight()
        {
            Vector2 StarPos = new Vector2(Random.Range(0, Camera.main.pixelWidth), Camera.main.pixelHeight);
            Vector2 WorldStarPos = Camera.main.ScreenToWorldPoint(StarPos);

            Instantiate(StarLightGameObject, WorldStarPos, Quaternion.identity);
        }

        void DocsaChim(TwitchCommandData commandData)
        {
            Character.Docsa docsaSakki;
            if (DocsaDict.TryGetValue(commandData.Author, out docsaSakki))
            {
                docsaSakki.Chim(HunterDict.Values.GetEnumerator().Current);
            } else
            {
                print("그런 독사 없음");
            }
        }

        void DocsaJump(TwitchCommandData commandData)
        {
            Character.Docsa docsaSakki;
            if (DocsaDict.TryGetValue(commandData.Author, out docsaSakki))
            {
                // docsaSakki.Behaviour.JumpHead();
            } else
            {
                print("그런 독사 없음");
            }
        }

        void HunterNet(TwitchCommandData commandData)
        {
            Character.Hunter docsaSakki;
            if (HunterDict.TryGetValue(commandData.Author, out docsaSakki))
            {
                // docsaSakki.Behaviour.ThrowNet();
            } else
            {
                print("그런 헌터 없음");
            }
        }

        void HunterAttack(TwitchCommandData commandData)
        {
            Character.Hunter docsaSakki;
            if (HunterDict.TryGetValue(commandData.Author, out docsaSakki))
            {
                // docsaSakki.Behaviour.Attack();
            } else
            {
                print("그런 헌터 없음");
            }
        }
    }
}
