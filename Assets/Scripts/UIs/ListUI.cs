using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Utility;
using Utility.UI;

using TMPro;

namespace Docsa
{
    [System.Serializable] public class DocsaListItemDict : SerializableDictionary<string, DocsaListItem> {}
    public class ListUI : MonoBehaviour, IDocsaSakkiManagerListener
    {
        public Canvas UICanvas;
        public GameObject ListUIObject;
        public GameObject ListItemPrefab;
        public VerticalLayoutGroup AttendingDocsaList;
        public VerticalLayoutGroup AttendingHunterList;
        public VerticalLayoutGroup WaitingViewerList;
        [SerializeField] DocsaListItemDict _listItemDict = new DocsaListItemDict();

        bool isInitializing;

        protected virtual void Reset()
        {
            if (ListUIObject)
                ListUIObject.SetActive(false);
        }

        protected virtual void Awake()
        {
            DocsaSakkiManager.instance.Listeners.Add(this);
        }

        public virtual void AddListItem(DocsaData data)
        {
            GameObject item = null;
            switch (data.State)
            {
                case DocsaData.DocsaState.Waiting:
                    item = Instantiate(ListItemPrefab, WaitingViewerList.transform);
                break;
                case DocsaData.DocsaState.Docsa:
                    item = Instantiate(ListItemPrefab, AttendingDocsaList.transform);
                break;
                case DocsaData.DocsaState.Hunter:
                    item = Instantiate(ListItemPrefab, AttendingHunterList.transform);
                break;
            }

            DocsaListItem listItem = item.GetComponent<DocsaListItem>();
            _listItemDict.Add(data.Author, listItem);
            SetDocsaData(data.Author, data);
        }

        public void RemoveListItem(DocsaListItem listItem)
        {
            RemoveListItem(listItem.DocsaData);
        }

        public virtual void RemoveListItem(DocsaData data)
        {
            DocsaListItem listItem;
            if (_listItemDict.TryGetValue(data.Author, out listItem))
            {
                if (DocsaSakkiManager.instance.GetDocsaData(data.Author) != null)
                    DocsaSakkiManager.instance.Kick(data.Author);

                _listItemDict.Remove(data.Author);
                Destroy(listItem.gameObject);
            } else
            {
                return;
            }
        }

        public virtual void SetDocsaData(string key, DocsaData newData)
        {
            DocsaListItem docsaListItem;
            if (_listItemDict.TryGetValue(key, out docsaListItem))
            {
                docsaListItem.DocsaData = newData;
                docsaListItem.DragAndDropableUI.Canvas = UICanvas;
                docsaListItem.Text.text = newData.Author;
            }
            // Todo
            // DocsaSakkiManager.instance.Set()

            MoveDocsaDataCardTo(newData, newData.State);
        }

        public virtual void MoveDocsaDataCardTo(string author, DocsaData.DocsaState to)
        {
            switch (to)
            {
                case DocsaData.DocsaState.Docsa :
                _listItemDict[author].transform.parent.SetParent(AttendingDocsaList.transform);
                break;
                case DocsaData.DocsaState.Hunter :
                _listItemDict[author].transform.parent.SetParent(AttendingHunterList.transform);
                break;
                case DocsaData.DocsaState.Waiting :
                _listItemDict[author].transform.parent.SetParent(WaitingViewerList.transform);
                break;
            }
        }

        public void MoveDocsaDataCardTo(DocsaData docsaData, DocsaData.DocsaState to)
        {
            MoveDocsaDataCardTo(docsaData.Author, to);
        }

        public virtual void RandomDistribute()
        {
            DocsaData[] datas = DocsaSakkiManager.instance.GetRandomWaitingDocsaDatas(Chunk.Current.DocsaCount);
            foreach (DocsaData data in datas)
            {
                MoveDocsaDataCardTo(data, DocsaData.DocsaState.Docsa);
                DocsaSakkiManager.instance.MoveDocsaDataTo(data, DocsaData.DocsaState.Docsa);
            }

            datas = DocsaSakkiManager.instance.GetRandomWaitingDocsaDatas(Chunk.Current.HunterCount);
            foreach (DocsaData data in datas)
            {
                MoveDocsaDataCardTo(data, DocsaData.DocsaState.Hunter);
                DocsaSakkiManager.instance.MoveDocsaDataTo(data, DocsaData.DocsaState.Hunter);
            }
        }

        public virtual void Listene(DocsaData data)
        {
            // Attend, Exit, Kick, Move
            DocsaListItem listItem = null;
            if (_listItemDict.TryGetValue(data.Author, out listItem))
            {
                if (data.State == DocsaData.DocsaState.Exit)
                {
                    RemoveListItem(data);
                } else
                {
                    SetDocsaData(data.Author, data);
                }
            } else
            {
                if (data.State != DocsaData.DocsaState.Exit)
                {
                    AddListItem(data);
                }
            }
        }
    }
}