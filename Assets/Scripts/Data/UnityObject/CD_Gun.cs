using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Gun", menuName = "Picker3D/CD_Gun", order = 0)]
    public class CD_Gun : ScriptableObject
    {
        public AllGunsData Data;
        public List<int> Levels;
    }
}