using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using Utility;
using UnityEngine;

public class StarRain : MonoBehaviour
{
    PoolSettings StarRainPoolSetting = new PoolSettings{
        PoolName = "StarRain",
        SourceFilePath = "Star",
        MaxPoolSize = 30,
        AutoReturn = true,
        AutoReturnTime = 3,
    };
    public GameObject StarPrefab;

    void Awake()
    {
        ObjectPool pool;
        pool = ObjectPool.GetOrCreate(StarRainPoolSetting.PoolName);
        pool.SourceObject = StarPrefab;
        pool.Init(StarRainPoolSetting);
    }
}
