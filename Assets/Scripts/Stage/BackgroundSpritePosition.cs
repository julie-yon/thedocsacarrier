using UnityEngine;

namespace Docsa
{
    public class BackgroundSpritePosition : MonoBehaviour
    {
        private SpriteRenderer _renderer;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            _renderer.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f);
        }
    }
}