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
        public DocsaDataDict WaitingViewerDict = new DocsaDataDict();
        public DocsaDataDict AttendingDocsaDict = new DocsaDataDict();
        public DocsaDataDict AttendingHunterDict = new DocsaDataDict();
        public bool DocsaCanAttend;
        public int WaitingViewerLimit = 20;
        public int AttendingDocsaLimit
        {
            get {return Chunk.ActiveDocsaList.Count;}
        }
        public int AttendingHunterLimit
        {
            get {return Chunk.ActiveHunterList.Count;}
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
                if (PerkManager.instance.Data.ViewerAttendPerk.enabled)
                Attend(commandData);
                break;
                case DocsaTwitchCommand.EXIT:
                if (PerkManager.instance.Data.ViewerExitPerk.enabled)
                Exit(commandData);
                break;
                case DocsaTwitchCommand.STARLIGHT:
                if (PerkManager.instance.Data.StarLightPerk.enabled)
                StarRain();
                break;

                case DocsaTwitchCommand.DOCSA_ATTACK:
                if (PerkManager.instance.Data.DocsaChimPerk.enabled)
                DocsaChim(commandData);
                break;
                case DocsaTwitchCommand.DOCSA_JUMP:
                if (PerkManager.instance.Data.DocsaJumpPerk.enabled)
                DocsaJump(commandData);
                break;
                case DocsaTwitchCommand.HUNTER_NET:
                if (PerkManager.instance.Data.HunterNetPerk.enabled)
                HunterNet(commandData);
                break;
                case DocsaTwitchCommand.HUNTER_Jump:
                if (PerkManager.instance.Data.HunterJumpPerk.enabled)
                HunterJump(commandData);
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
            if (!DocsaCanAttend || WaitingViewerDict.Count >= WaitingViewerLimit)
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

            data = new DocsaData(commandData.Author);
            WaitingViewerDict.Add(data.Author, data);
        }

        /// <summary>
        /// Assign Viewers linearly.
        /// </summary>
        /// <param name="onlyAttending">Assign after moving data to attending if false.</param>
        public void AssignViewers(bool onlyAttending = true)
        {
            DocsaData[] datas = AttendingDocsaDict.Values.ToArray<DocsaData>();

            if (onlyAttending)
            {
                for (int i = 0; i < AttendingDocsaDict.Count && i < Chunk.ActiveDocsaList.Count; i++)
                {
                    AssignViewer(datas[i], Chunk.ActiveDocsaList[i]);
                }

                datas = AttendingHunterDict.Values.ToArray<DocsaData>();
                for (int i = 0; i < AttendingHunterDict.Count && i < Chunk.ActiveHunterList.Count; i++)
                {
                    AssignViewer(datas[i], Chunk.ActiveHunterList[i]);
                }
            } else
            {
                datas = WaitingViewerDict.Values.ToArray<DocsaData>();
                for (int i = 0; i < WaitingViewerDict.Count; i++)
                {
                    if (i < Chunk.ActiveDocsaList.Count)
                    {
                        MoveDocsaDataTo(datas[i], DocsaData.DocsaState.Docsa);
                    } else
                    {
                        MoveDocsaDataTo(datas[i], DocsaData.DocsaState.Hunter);
                    }
                }
                AssignViewers(!onlyAttending);
            }
        }

        /// <summary>
        /// Assign viewer Randomly with string
        /// </summary>
        public void AssignViewer(string viewerName)
        {
            AssignViewer(GetDocsaData(viewerName));
        }

        /// <summary>
        /// Assign viewer Randomly with DocsaData
        /// </summary>
        public void AssignViewer(DocsaData docsaData)
        {
            switch (docsaData.State)
            {
                case DocsaData.DocsaState.Docsa:
                    foreach(var v in Chunk.ActiveDocsaList)
                    {
                        if (!v.isViewerAssigned)
                        {
                            AssignViewer(docsaData, v);
                        }
                        return;
                    }
                break;
                case DocsaData.DocsaState.Hunter:
                    foreach(var v in Chunk.ActiveHunterList)
                    {
                        if (!v.isViewerAssigned)
                        {
                            AssignViewer(docsaData, v);
                        }
                        return;
                    }
                break;
                case DocsaData.DocsaState.Waiting:
                    foreach(var v in Chunk.ActiveDocsaList)
                    {
                        if (!v.isViewerAssigned)
                        {
                            AssignViewer(docsaData, v);
                        }
                        return;
                    }
                    foreach(var v in Chunk.ActiveHunterList)
                    {
                        if (!v.isViewerAssigned)
                        {
                            AssignViewer(docsaData, v);
                        }
                        return;
                    }
                break;
            }
        }

        public void AssignViewer(string viewerName, ViewerCharacter character)
        {
            AssignViewer(GetDocsaData(viewerName), character);
        }

        public void AssignViewer(DocsaData docsaData, ViewerCharacter character)
        {
            if (character.isViewerAssigned)
            {
                Debug.Log("Already assigned Character");
                return;
            }

            docsaData.Character = character;
            character.isViewerAssigned = true;
            character.ViewerName = docsaData.Author;
        }

        void Exit(TwitchCommandData commandData)
        {
            DocsaData data;
            if (((data = GetDocsaData(commandData.Author)) == null))
            {
                return;
            }
            

            // 게임중간에 Exit할경우 or
            // 일정시간이상 응답이 없을경우

            MoveDocsaDataTo(commandData.Author, DocsaData.DocsaState.Exit);

            WaitingViewerDict.Remove(data.Author);
        }

        public void Kick(string viewer)
        {
            if (AttendingDocsaDict.ContainsKey(viewer))
            {
                MoveDocsaDataTo(viewer, DocsaData.DocsaState.Exit);
                AttendingDocsaDict.Remove(viewer);

                // Auto Assign?

                // DocsaSakki docsa = (DocsaSakki)AttendingDocsaDict[viewer].Character;
                // DocsaData docsaData = AttendingDocsaDict[viewer];
                // if (DocsaCanAttend)
                // {
                //     docsa.ViewerName = WaitingViewerDict.GetEnumerator().Current.Value.Author;
                //     AttendingDocsaDict.Add(docsa.ViewerName, docsaData);
                // }

            } else if (AttendingHunterDict.ContainsKey(viewer))
            {
                MoveDocsaDataTo(viewer, DocsaData.DocsaState.Exit);
                AttendingHunterDict.Remove(viewer);

                // Auto Assign?

                // if (DocsaCanAttend)
                // Hunter hunter = (Hunter)AttendingHunterDict[viewer].Character;
                // DocsaData hunterData = AttendingDocsaDict[viewer];
                // {
                //     hunter.ViewerName = WaitingViewerDict.GetEnumerator().Current.Value.Author;
                //     AttendingHunterDict.Add(hunter.ViewerName, hunterData);
                // }

            } else if (WaitingViewerDict.ContainsKey(viewer))
            {
                MoveDocsaDataTo(viewer, DocsaData.DocsaState.Exit);
                WaitingViewerDict.Remove(viewer);
            } else
            {
                return;
            }
            
        }

        public void MoveDocsaDataTo(string author, DocsaData.DocsaState to)
        {
            MoveDocsaDataTo(GetDocsaData(author), to);
        }

        public void MoveDocsaDataTo(DocsaData from, DocsaData.DocsaState to)
        {
            if (from == null || GetDocsaData(from.Author) == null)
            {
                return;
            }
            
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

            if (data == null)
            {
                Debug.LogWarning("You are trying to get null docsaData. Check if it was test");
            }

            return data;
        }

        public void SetDocsaData(string author, DocsaData newData)
        {
            DocsaData data;
            if (WaitingViewerDict.TryGetValue(author, out data)) {}
            else if (AttendingDocsaDict.TryGetValue(author, out data)) {}
            else if (AttendingHunterDict.TryGetValue(author, out data)) {}
            data = newData;
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

            ObjectPool.GetOrCreate(DocsaPoolType.StarRain).Instantiate(WorldStarPos, Quaternion.identity);
        }

        void DocsaChim(TwitchCommandData commandData)
        {
            DocsaData docsaSakki;
            if (AttendingDocsaDict.TryGetValue(commandData.Author, out docsaSakki))
            {
                // for presentation
                ((DocsaSakki)docsaSakki.Character).Behaviour.Attack(docsaSakki.Character.transform.forward);
                //
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
                docsaSakki.Character.Behaviour.Attack(docsaSakki.Character.transform.forward);
            } else
            {
                print("그런 헌터 없음");
            }
            docsaSakki.Character.SetChatData(commandData.Chat);
        }

        void HunterJump(TwitchCommandData commandData)
        {
            DocsaData docsaSakki;
            if (AttendingHunterDict.TryGetValue(commandData.Author, out docsaSakki))
            {
                docsaSakki.Character.Behaviour.JumpHead();
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
            Exit,
        }
    }
}
