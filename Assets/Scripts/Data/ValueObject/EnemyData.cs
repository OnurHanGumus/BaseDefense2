using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyData
    {
        public float Speed = 8f;
        public int Damage = 5;
        public int Health = 100;
        public float DestroyDelay = 1.5f;
    }
}