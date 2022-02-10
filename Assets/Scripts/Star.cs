using UnityEngine;

using Utility;

namespace Docsa
{
    public class Star : MonoBehaviour
    {
        void Return()
        {
            ObjectPool.GetOrCreate(DocsaPoolType.StarRain).Return(gameObject);
        }
    }
}