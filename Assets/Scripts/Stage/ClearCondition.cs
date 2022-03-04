using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Docsa.Character;
using Utility;

namespace Docsa
{
    public class ClearCondition
    {
        public delegate bool CheckIfTrue(Chunk chunk);
        public CheckIfTrue Fulfilled;

        public ClearCondition(CheckIfTrue fulfill)
        {
            Fulfilled = fulfill;
        }
    }
}