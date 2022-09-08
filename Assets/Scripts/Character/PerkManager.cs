using System;

using UnityEngine;

namespace Docsa
{
    public class PerkManager : MonoBehaviour
    {
        private static PerkManager _instance;
        public static PerkManager instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = FindObjectOfType(typeof(PerkManager)) as PerkManager;
                        if (_instance == null) throw new NullReferenceException();
                    } catch (NullReferenceException)
                    {
                        Debug.LogWarning("There's no active " + typeof(PerkManager) + " in this scene\n Made new temporary instance");
                        _instance = new GameObject("TemporaryPerkManager").AddComponent<PerkManager>();
                        _instance.Data = Resources.Load<PerkData>("Scriptables/PerkDatas/Test");
                    }
                }

                return _instance;
            }
        }

        public PerkData Data;

        public void ChangePerkData(PerkData data)
        {
            Data = data;
        }
    }
}