using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Object = UnityEngine.Object;

public static class DontDestroyObjects
{
    public static List<Object> Objects = new List<Object>();

    public static int Count
    {
        get{return Objects.Count;}
    }

    public static bool isEmpty
    {
        get{return Objects.Count == 0;}
    }

    public static void Add(Object obj)
    {
        if (ContainType(obj.GetType()))
        {
            MonoBehaviour.Destroy(((Component)obj).gameObject);
            return;
        }

        Objects.Add(obj);
        MonoBehaviour.DontDestroyOnLoad(obj);
    }

    public static void Remove(Object obj)
    {
        Objects.Remove(obj);
    }

    public static bool Contain(Object obj)
    {
        return Objects.Contains(obj);
    }

    private static bool ContainType(Type type)
    {
        foreach(var obj in Objects)
        {
            if (type.IsInstanceOfType(obj))
            {
                return true;
            }
        }

        return false;
    }
}
