using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using TwitchIRC;

namespace Docsa
{
    public class RandomItem : Item
    {
        #pragma warning disable 0108
        void Awake()
        {
            ItemEvent.AddListener(Effect);
        }

        public override void Effect()
        {
            print("Random Item Effect");

            base.Effect();
        }
    }
}