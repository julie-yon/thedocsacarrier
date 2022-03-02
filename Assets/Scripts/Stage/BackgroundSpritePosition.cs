using UnityEngine;

namespace Docsa
{
    public class BackgroundSpritePosition : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private float _baseCameSize;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _baseCameSize = Camera.main.orthographicSize;
        }

        void Update()
        {
            _renderer.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f);

            float scaleValue = Camera.main.orthographicSize / _baseCameSize;
            _renderer.transform.localScale = new Vector3(scaleValue, scaleValue, 1);
        }
    }
}