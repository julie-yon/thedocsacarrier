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
        [SerializeField] private Core _core;
        public DocsaDataDict WaitingViewerDict = new DocsaDataDict();
        public DocsaDataDict AttendingDocsaDict = new DocsaDataDict();
        public DocsaDataDict AttendingHunterDict = new DocsaDataDict();
        public bool DocsaCanAttend;
        public int WaitingViewerLimit = 20;

        public bool CorrectlyAssigned
        {
            get
            {
                if (AttendingDocsaDict.Count != AttendingDocsaLimit || AttendingHunterDict.Count != AttendingHunterLimit) return false;
                
                foreach (KeyValuePair<string, DocsaData> pair in AttendingDocsaDict)
                {
                    if (pair.Value.Character == null) return false;
                }

                foreach (KeyValuePair<string, DocsaData> pair in AttendingDocsaDict)
                {
                    if (pair.Value.Character == null) return false;
                }

                return true;
            }
        }
        
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
            if (commandData.Author == _core.UzuhamaTwitchNickName)
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
                case DocsaTwitchCommand.HUNTER_NET:
                if (PerkManager.instance.Data.DocsaChimPerk.enabled)
                Attack(commandData);
                break;
                case DocsaTwitchCommand.DOCSA_JUMP:
                case DocsaTwitchCommand.HUNTER_Jump:
                if (PerkManager.instance.Data.DocsaJumpPerk.enabled)
                Jump(commandData);
                break;

                default :
                break;
            }
        }

        void UzuhamaChat(TwitchCommandData commandData)
        {
            UzuHama.Hama.SetChatData(commandData.Chat);
        }

        #region Assign Viewer
        /// <summary>
        /// Totally AssignViewer Method
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

        /// <summary>
        /// Final AssignViewer Method
        /// </summary>
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
        #endregion

        #region Commands Implementation
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
        }

        public void Kick(string viewer)
        {
            if (AttendingDocsaDict.ContainsKey(viewer))
            {
                MoveDocsaDataTo(viewer, DocsaData.DocsaState.Exit);

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
            } else
            {
                return;
            }
        }

        void StarRain()
        {
            print("StarRain");
            Vector2 StarPos = new Vector2(Random.Range(0, Camera.main.pixelWidth), Camera.main.pixelHeight);
            Vector2 WorldStarPos = Camera.main.ScreenToWorldPoint(StarPos);

            print("WorldStarPos" + WorldStarPos);

            ObjectPool.GetOrCreate(DocsaPoolType.StarRain).Instantiate(WorldStarPos, Quaternion.identity);
        }

        void Attack(TwitchCommandData commandData)
        {
            Character.Character character = GetCharacter(commandData.Author);
            if (character == null) {print("그런 독사 없음"); return;}

            character.Behaviour.Attack(character.transform.forward);
            character.SetChatData(commandData.Chat);
        }

        void Jump(TwitchCommandData commandData)
        {
            Character.Character character = GetCharacter(commandData.Author);
            if (character == null) {print("그런 독사 없음"); return;}
            
            character.Behaviour.Jump();
            character.SetChatData(commandData.Chat);
        }

        void NoneCommand(TwitchCommandData commandData)
        {
            GetCharacter(commandData.Author).SetChatData(commandData.FormattedChat);
        }
        #endregion

        #region DocsaData Managing
        public void MoveDocsaDataTo(string author, DocsaData.DocsaState to)
        {
            MoveDocsaDataTo(GetDocsaData(author), to);
        }

        public void MoveDocsaDataTo(DocsaData data, DocsaData.DocsaState to)
        {
            if (data == null || GetDocsaData(data) == null)
            {
                return;
            }
            
            switch (data.State)
            {
                case DocsaData.DocsaState.Waiting : 
                    WaitingViewerDict.Remove(data.Author);
                break;
                case DocsaData.DocsaState.Docsa : 
                    AttendingDocsaDict.Remove(data.Author);
                break;
                case DocsaData.DocsaState.Hunter : 
                    AttendingHunterDict.Remove(data.Author);
                break;
            }

            data.State = to;

            switch (to)
            {
                case DocsaData.DocsaState.Waiting :
                    data.Character = null;
                    WaitingViewerDict.Add(data.Author, data);
                break;
                case DocsaData.DocsaState.Docsa :
                    data.Character = null;
                    AttendingDocsaDict.Add(data.Author, data);
                break;
                case DocsaData.DocsaState.Hunter :
                    data.Character = null;
                    AttendingHunterDict.Add(data.Author, data);
                break;
            }
        }

        public DocsaData GetDocsaData(DocsaData data)
        {
            return GetDocsaData(data.Author);
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

            try
            {
                character = GetDocsaData(author).Character;
            } catch (System.NullReferenceException)
            {
                character = null;
            }
            
            return character;
        }
        #endregion

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
