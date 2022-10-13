using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Boss", menuName = "Picker3D/CD_Boss", order = 0)]
    public class CD_Boss : ScriptableObject
    {
        public BossData[] Data;
    }
}