using System.Collections;
using System.Collections.Generic;
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
                // if (_FloatingTextCanvas == null)
                // {
                //     _FloatingTextCanvas = FindObjectOfType(typeof(Canvas)) as Canvas;
                // }

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
        // Start is called before the first frame update
        public void MakeNewText(Vector3 posit, int damageValue)
        {
            MakeNewText(posit, damageValue.ToString());
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
