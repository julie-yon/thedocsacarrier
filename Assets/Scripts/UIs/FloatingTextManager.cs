using UnityEngine;
using Utility;

namespace Docsa
{
    public class FloatingTextManager : Singleton<FloatingTextManager>
    {
        private static Canvas _FloatingTextCanvas;
        public static Canvas FloatingTextCanvas
        {
            get
            {
                if (_FloatingTextCanvas == null)
                {
                    GameObject go = new GameObject("FloatingTextCanvas");
                    _FloatingTextCanvas = go.AddComponent<Canvas>();
                    _FloatingTextCanvas.renderMode = RenderMode.WorldSpace;
                }

                return _FloatingTextCanvas;
            }
        }
        
        public GameObject FloatingTextPref;

        public void MakeNewDamageText(Vector3 position, int damageValue)
        {
            MakeNewText(position, damageValue.ToString());
        }
        
        public void MakeNewText(Vector3 posit, string text)
        {
            GameObject floatingText = Instantiate(FloatingTextPref, posit, Quaternion.identity );
            FloatingText floatingTextComponent = floatingText.GetComponent<FloatingText>();
            floatingTextComponent.content = text;
            floatingTextComponent.transform.SetParent(FloatingTextCanvas.transform);
        }
        
    }
}
