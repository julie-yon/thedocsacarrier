using UnityEngine;
using UnityEngine.UI;

using dkstlzu.Utility.UI;
using TMPro;

namespace Docsa
{
    public class DocsaListItem : MonoBehaviour
    {
        public DocsaData DocsaData;
        public TextMeshProUGUI Text;
        public DragAndDropableUI DragAndDropableUI;

        public string Author
        {
            get {return DocsaData.Author;}
            set {DocsaData.Author = value; Text.text = value;}
        }
    }
}