using UnityEngine;

namespace Utility
{
    public class ObjectPoolInitializer : MonoBehaviour
    {
        public PoolSettings ObjectPoolSetting = new PoolSettings{};
        public GameObject SourcePrefab;
        public bool MakeObjectPoolOnThisGameObject;

        void Awake()
        {
            ObjectPool pool;
            if (MakeObjectPoolOnThisGameObject)
            {
                pool = ObjectPool.GetOrCreate(ObjectPoolSetting.PoolName, gameObject);
            } else
            {
                pool = ObjectPool.GetOrCreate(ObjectPoolSetting.PoolName);
            }
            pool.SourceObject = SourcePrefab;
            pool.Init(ObjectPoolSetting);
        }
    }
}