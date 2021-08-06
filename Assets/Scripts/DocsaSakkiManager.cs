using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchIRC;
using Utility;

using Docsa.Character;

namespace Docsa
{
    [System.Serializable]
    public class DocsaDataDict : SerializableDictionary<string, DocsaData> {}
    public class DocsaSakkiManager : Singleton<DocsaSakkiManager>
    {
        public DocsaDataDict WaitingViewerDict;
        public DocsaDataDict AttendingDocsaDict;
        public DocsaDataDict AttendingHunterDict;
        public bool DocsaCanAttend;
        public int WaitingViewerLimit = 20;
        
        void Awake()
        {
            DontDestroyObjects.Add(this);
            WaitingViewerDict = new DocsaDataDict();
            AttendingDocsaDict = new DocsaDataDict();
            AttendingHunterDict = new DocsaDataDict();
        }

        public void ExecuteCommand(TwitchCommandData commandData)
        {
            if (commandData.Author == Core.instance.UzuhamaTwitchNickName)
            {
                UzuhamaChat(commandData);
                return;
            }

            switch (commandData.Command)
            {
                case DocsaTwitchCommand.NONE:
                NoneCommand(commandData);
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

                default :
                break;
            }
        }

        void UzuhamaChat(TwitchCommandData commandData)
        {
            UzuHama.Hama.SetChatData(commandData.Chat);
        }

        void Attend(TwitchCommandData commandData)
        {
            if (!DocsaCanAttend || WaitingViewerDict.Count > WaitingViewerLimit)
            {
                return;
            }

            if (AttendingDocsaDict.ContainsKey(commandData.Author) || AttendingHunterDict.ContainsKey(commandData.Author))
            {
                return;
            }

            DocsaData data;

            if (WaitingViewerDict.TryGetValue(commandData.Author, out data))
            {
                data.ChatCount++;
                return;
            }

            // Docsa 참여와 Hunter 참여를 어떻게 구분할지
            // 1. 명령어를 나눈다
            // 2. 랜덤하게 임의로 나눈다.
            data = new DocsaData(commandData.Author);
            WaitingViewerDict.Add(data.Author, data);
            ESCUIManager.instance.AddWaitingViewer(WaitingViewerDict[data.Author]);
        }

        void Exit(TwitchCommandData commandData)
        {
            if (!WaitingViewerDict.ContainsKey(commandData.Author) && !AttendingDocsaDict.ContainsKey(commandData.Author) && !AttendingHunterDict.ContainsKey(commandData.Author))
            {
                return;
            }
            

            // 게임중간에 Exit할경우 or
            // 일정시간이상 응답이 없을경우

            WaitingViewerDict.Remove(commandData.Author);
        }

        public void Kick(string viewer)
        {
            if (AttendingDocsaDict.ContainsKey(viewer))
            {
                DocsaSakki docsa = (DocsaSakki)AttendingDocsaDict[viewer].Character;
                DocsaData data = AttendingDocsaDict[viewer];
                AttendingDocsaDict.Remove(viewer);

                if (DocsaCanAttend)
                {
                    docsa.Author = WaitingViewerDict.GetEnumerator().Current.Value.Author;
                    AttendingDocsaDict.Add(docsa.Author, data);
                }

                return;
            }

            if (AttendingHunterDict.ContainsKey(viewer))
            {
                Hunter hunter = (Hunter)AttendingHunterDict[viewer].Character;
                DocsaData data = AttendingDocsaDict[viewer];
                AttendingHunterDict.Remove(viewer);

                if (DocsaCanAttend)
                {
                    hunter.Author = WaitingViewerDict.GetEnumerator().Current.Value.Author;
                    AttendingHunterDict.Add(hunter.Author, data);
                }

                return;
            }

            if (WaitingViewerDict.ContainsKey(viewer))
            {
                DocsaData docsa = WaitingViewerDict[viewer];
                WaitingViewerDict.Remove(viewer);

                return;
            }
        }

        public void MoveDocsaDataTo(string author, DocsaData.DocsaState to)
        {
            MoveDocsaDataTo(GetDocsaData(author), to);
        }

        public void MoveDocsaDataTo(DocsaData from, DocsaData.DocsaState to)
        {
            switch (from.State)
            {
                case DocsaData.DocsaState.Waiting : 
                    WaitingViewerDict.Remove(from.Author);
                break;
                case DocsaData.DocsaState.Docsa : 
                    AttendingDocsaDict.Remove(from.Author);
                break;
                case DocsaData.DocsaState.Hunter : 
                    AttendingHunterDict.Remove(from.Author);
                break;
            }

            from.State = to;

            switch (to)
            {
                case DocsaData.DocsaState.Waiting :
                    WaitingViewerDict.Add(from.Author, from);
                break;
                case DocsaData.DocsaState.Docsa :
                    AttendingDocsaDict.Add(from.Author, from);
                break;
                case DocsaData.DocsaState.Hunter :
                    AttendingHunterDict.Add(from.Author, from);
                break;
            }
        }

        public DocsaData GetDocsaData(string author)
        {
            DocsaData data;
            if (WaitingViewerDict.TryGetValue(author, out data))
            {

            } else if (AttendingDocsaDict.TryGetValue(author, out data))
            {
                
            } else if (AttendingHunterDict.TryGetValue(author, out data))
            {

            }

            return data;
        }

        public DocsaData[] GetRandomWaitingDocsaDatas(int number)
        {
            HashSet<DocsaData> datas = new HashSet<DocsaData>();

            DocsaData[] totalDatas = WaitingViewerDict.Values.ToArray<DocsaData>();

            int capacity = Mathf.Min(number, totalDatas.Length);

            while (datas.Count < capacity)
            {
                int index = (int)Random.Range(0, totalDatas.Length);
                datas.Add(totalDatas[index]);
            }

            return datas.ToArray();
        }

        public Docsa.Character.Character GetCharacter(string author)
        {
            Docsa.Character.Character character;

            character = GetDocsaData(author).Character;
            
            return character;
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
            DocsaData docsaSakki;
            if (AttendingDocsaDict.TryGetValue(commandData.Author, out docsaSakki))
            {
                ((DocsaSakki)docsaSakki.Character).Chim((Hunter)AttendingHunterDict.Values.GetEnumerator().Current.Character);
            } else
            {
                print("그런 독사 없음");
            }
            docsaSakki.Character.SetChatData(commandData.Chat);
        }

        void DocsaJump(TwitchCommandData commandData)
        {
            DocsaData docsaSakki;
            if (AttendingDocsaDict.TryGetValue(commandData.Author, out docsaSakki))
            {
                docsaSakki.Character.Behaviour.JumpHead();
            } else
            {
                print("그런 독사 없음");
            }
            docsaSakki.Character.SetChatData(commandData.Chat);
        }

        void HunterNet(TwitchCommandData commandData)
        {
            DocsaData docsaSakki;
            if (AttendingHunterDict.TryGetValue(commandData.Author, out docsaSakki))
            {
                // docsaSakki.Character.Behaviour.ThrowNet();
            } else
            {
                print("그런 헌터 없음");
            }
            docsaSakki.Character.SetChatData(commandData.Chat);
        }

        void HunterAttack(TwitchCommandData commandData)
        {
            DocsaData docsaSakki;
            if (AttendingHunterDict.TryGetValue(commandData.Author, out docsaSakki))
            {
                // docsaSakki.Character.Behaviour.Attack();
            } else
            {
                print("그런 헌터 없음");
            }
            docsaSakki.Character.SetChatData(commandData.Chat);
        }

        void NoneCommand(TwitchCommandData commandData)
        {
            GetCharacter(commandData.Author).SetChatData(TwitchCommandData.Prefix + commandData.Command + " " + commandData.Chat);
        }
    }

    [System.Serializable]
    public class DocsaData
    {
        public string Author;
        public Docsa.Character.Character Character;
        public int ChatCount;
        public DocsaState State;

        public DocsaData(string author)
        {
            Author = author;
            ChatCount = 1;
            State = DocsaState.Waiting;
        }

        public DocsaData(string author, Docsa.Character.Character character)
        {
            Author = author;
            ChatCount = 1;
            Character = character;
            State = DocsaState.Waiting;
        }

        public enum DocsaState
        {
            Waiting,
            Docsa,
            Hunter,
        }
    }
}
