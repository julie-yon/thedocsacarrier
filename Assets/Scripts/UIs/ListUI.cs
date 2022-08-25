using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using dkstlzu.Utility;
using dkstlzu.Utility.UI;

using TMPro;

namespace Docsa
{
    public class ListUI : MonoBehaviour
    {
        public GameObject ListItemPrefab;
        public VerticalLayoutGroup AttendingDocsaList;
        public VerticalLayoutGroup AttendingHunterList;
        public VerticalLayoutGroup WaitingViewerList;
        public List<DocsaListItem> _listItemList = new List<DocsaListItem>();

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
            _listItemList.Add(listItem);
            SetDocsaData(data.Author, data);
        }

        public void RemoveListItem(DocsaListItem listItem)
        {
            _listItemList.Remove(listItem);
            Destroy(listItem.gameObject);
        }

        public virtual void RemoveListItem(DocsaData data)
        {
            RemoveListItem(GetDocsaListItem(data.Author));
        }

        public virtual void SetDocsaData(string author, DocsaData newData)
        {
            DocsaListItem docsaListItem = GetDocsaListItem(author);
            if (docsaListItem)
            {
                docsaListItem.DocsaData = newData;
                // Should Check and Fix
                // docsaListItem.DragAndDropableUI.CanvasScaleFactor = transform.root.GetComponent<CanvasScaler>().scaleFactor;
                docsaListItem.Text.text = newData.Author;
            }

            MoveDocsaDataCardTo(newData, newData.State);
        }

        public virtual void MoveDocsaDataCardTo(string author, DocsaData.DocsaState to)
        {
            DocsaListItem targetItem = GetDocsaListItem(author);
            if (targetItem == null) return;

            switch (to)
            {
                case DocsaData.DocsaState.Docsa :
                targetItem.transform.parent.SetParent(AttendingDocsaList.transform);
                break;
                case DocsaData.DocsaState.Hunter :
                targetItem.transform.parent.SetParent(AttendingHunterList.transform);
                break;
                case DocsaData.DocsaState.Waiting :
                targetItem.transform.parent.SetParent(WaitingViewerList.transform);
                break;
            }
        }

        private DocsaListItem GetDocsaListItem(string author)
        {
            DocsaListItem targetItem = null;
            foreach (var item in _listItemList)
            {
                if (item.DocsaData.Author == author) targetItem = item;
            }

            return targetItem;
        }

        public void MoveDocsaDataCardTo(DocsaData docsaData, DocsaData.DocsaState to)
        {
            MoveDocsaDataCardTo(docsaData.Author, to);
        }

        public virtual void RandomDistribute()
        {
            DocsaData[] datas = DocsaSakkiManager.instance.GetRandomWaitingDocsaDatas(Chunk.ActiveDocsaList.Count);
            foreach (DocsaData data in datas)
            {
                MoveDocsaDataCardTo(data, DocsaData.DocsaState.Docsa);
                DocsaSakkiManager.instance.MoveDocsaDataTo(data, DocsaData.DocsaState.Docsa);
            }

            datas = DocsaSakkiManager.instance.GetRandomWaitingDocsaDatas(Chunk.ActiveHunterList.Count);
            foreach (DocsaData data in datas)
            {
                MoveDocsaDataCardTo(data, DocsaData.DocsaState.Hunter);
                DocsaSakkiManager.instance.MoveDocsaDataTo(data, DocsaData.DocsaState.Hunter);
            }
        }

        public virtual void UpdateData()
        {
            if (!DocsaSakkiManager.instance) return;

            int waitingCount = DocsaSakkiManager.instance.WaitingViewerDict.Count;
            int docsaCount = DocsaSakkiManager.instance.AttendingDocsaDict.Count;
            int hunterCount = DocsaSakkiManager.instance.AttendingHunterDict.Count;

            if (_listItemList.Count < waitingCount + docsaCount + hunterCount)
            {
                for (int i = _listItemList.Count; i < waitingCount + docsaCount + hunterCount; i++)
                {
                    AddListItem(new DocsaData("Dummy" + i));
                }
            } else if (_listItemList.Count > waitingCount + docsaCount + hunterCount)
            {
                for (int i = waitingCount + docsaCount + hunterCount; i < _listItemList.Count;)
                {
                    RemoveListItem(_listItemList[i]);
                }
            }

            for (int i = 0; i < waitingCount; i++)
            {
                MoveDocsaDataCardTo(_listItemList[i].DocsaData, DocsaData.DocsaState.Waiting);
            }
            

            for (int i = 0; i < docsaCount; i++)
            {
                MoveDocsaDataCardTo(_listItemList[i].DocsaData, DocsaData.DocsaState.Docsa);
            }

            for (int i = 0; i < hunterCount; i++)
            {
                MoveDocsaDataCardTo(_listItemList[i].DocsaData, DocsaData.DocsaState.Hunter);
            }

            SetNames();

            void SetNames()
            {
                var waitingIter = DocsaSakkiManager.instance.WaitingViewerDict.GetEnumerator();
                foreach (var listItem in WaitingViewerList.GetComponentsInChildren<DocsaListItem>())
                {
                    if (!waitingIter.MoveNext()) break;
                    listItem.Author = waitingIter.Current.Value.Author;
                    SetDocsaData(listItem.Author, listItem.DocsaData);
                }
                var docsaIter = DocsaSakkiManager.instance.AttendingDocsaDict.GetEnumerator();
                foreach (var listItem in AttendingDocsaList.GetComponentsInChildren<DocsaListItem>())
                {
                    if (!waitingIter.MoveNext()) break;
                    listItem.Author = waitingIter.Current.Value.Author;
                    SetDocsaData(listItem.Author, listItem.DocsaData);
                }
                var hunterIter = DocsaSakkiManager.instance.AttendingHunterDict.GetEnumerator();
                foreach (var listItem in AttendingHunterList.GetComponentsInChildren<DocsaListItem>())
                {
                    if (!waitingIter.MoveNext()) break;
                    listItem.Author = waitingIter.Current.Value.Author;
                    SetDocsaData(listItem.Author, listItem.DocsaData);
                }
            }
        }
    }
}