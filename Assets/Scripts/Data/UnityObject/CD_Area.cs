using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Area", menuName = "Picker3D/CD_Area", order = 0)]
    public class CD_Area : ScriptableObject
    {
        public TotalLevelAreaData totalLevelAreaData;
    }
}