using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public float Speed = 2f;
        public float RotationSpeed = 2f;
        public int Health = 100;
    }
}