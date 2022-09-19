using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class StackData
    {
        public float AddSpeed = 10f;
        public int Capacity = 10;
        public float DistanceFormPlayer = 1f;
        public float OffsetY = 1f;
        public float OffsetZ = 1f;
    }
}