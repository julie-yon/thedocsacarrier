using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchIRC;
using Utility;

using Docsa.Character;

namespace Docsa
{
    [System.Serializable]
    public class WaitingDataDict : SerializableDictionary<string, WaitingData> {}
    [System.Serializable]
    public class DocsaDict : SerializableDictionary<string, DocsaSakki> {}
    [System.Serializable]
    public class HunterDict : SerializableDictionary<string, Hunter> {}
    public class DocsaSakkiManager : Singleton<DocsaSakkiManager>
    {
        public WaitingDataDict WaitingViewerDict;
        public DocsaDict AttendingDocsaDict;
        public HunterDict AttendingHunterDict;

        private bool _docsaCanAttend;
        
        void Awake()
        {
            WaitingViewerDict = new WaitingDataDict();
            AttendingDocsaDict = new DocsaDict();
            AttendingHunterDict = new HunterDict();
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
                StarRain();
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
            if (!_docsaCanAttend)
            {
                return;
            }

            if (WaitingViewerDict.ContainsKey(commandData.Author))
            {
                return;
            }

            // Docsa 참여와 Hunter 참여를 어떻게 구분할지
            // 1. 명령어를 나눈다
            // 2. 랜덤하게 임의로 나눈다.
            WaitingViewerDict.Add(commandData.Author, new WaitingData(commandData.Author));
        }

        void Exit(TwitchCommandData commandData)
        {
            if (!WaitingViewerDict.ContainsKey(commandData.Author))
            {
                return;
            }

            // 게임중간에 Exit할경우 or
            // 일정시간이상 응답이 없을경우
            // 하마가 강퇴한경우

            WaitingViewerDict.Remove(commandData.Author);
        }

        void StarRain()
        {
            print("StarRain");
            Vector2 StarPos = new Vector2(Random.Range(0, Camera.main.pixelWidth), Camera.main.pixelHeight);
            Vector2 WorldStarPos = Camera.main.ScreenToWorldPoint(StarPos);

            print("WorldStarPos" + WorldStarPos);

            ObjectPool.SPoolDict[PoolType.StarRain].Instantiate(WorldStarPos, Quaternion.identity);
        }

        void DocsaChim(TwitchCommandData commandData)
        {
            DocsaSakki docsaSakki;
            if (AttendingDocsaDict.TryGetValue(commandData.Author, out docsaSakki))
            {
                docsaSakki.Chim(AttendingHunterDict.Values.GetEnumerator().Current);
            } else
            {
                print("그런 독사 없음");
            }
        }

        void DocsaJump(TwitchCommandData commandData)
        {
            DocsaSakki docsaSakki;
            if (AttendingDocsaDict.TryGetValue(commandData.Author, out docsaSakki))
            {
                docsaSakki.Behaviour.JumpHead();
            } else
            {
                print("그런 독사 없음");
            }
        }

        void HunterNet(TwitchCommandData commandData)
        {
            Hunter docsaSakki;
            if (AttendingHunterDict.TryGetValue(commandData.Author, out docsaSakki))
            {
                // docsaSakki.Behaviour.ThrowNet();
            } else
            {
                print("그런 헌터 없음");
            }
        }

        void HunterAttack(TwitchCommandData commandData)
        {
            Hunter docsaSakki;
            if (AttendingHunterDict.TryGetValue(commandData.Author, out docsaSakki))
            {
                // docsaSakki.Behaviour.Attack();
            } else
            {
                print("그런 헌터 없음");
            }
        }
    }

    [System.Serializable]
    public class WaitingData
    {
        public string Author;
        public TwitchCommandData LastCommand;
        public int ChatCount;

        public WaitingData(string author)
        {
            Author = author;
            ChatCount = 1;
        }
    }
}
