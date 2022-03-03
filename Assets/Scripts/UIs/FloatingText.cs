using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Docsa
{
    public class FloatingText : TextMeshProUGUI
    {
        private float _timeElapsed;
        public string content;


        protected override void Start()
        {
            base.Start();
            _timeElapsed = 0;
            rectTransform.anchoredPosition = gameObject.transform.position;
        }
        
        private void Update()
        {
            if (content != null)
            {
                text = content;
                _timeElapsed += Time.deltaTime / 3;
                color = Color.Lerp(color, Color.clear, _timeElapsed*0.5f);
                fontSize = Mathf.Lerp(1, 0, _timeElapsed * 0.3f);
         
                if (color.a < 0.05f)
                {
                    Destroy(gameObject);
                }
            }
        }
        
    }
}
