using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Soldier", menuName = "Picker3D/CD_Soldier", order = 0)]
    public class CD_Soldier: ScriptableObject
    {
        public SoldierData Data;
    }
}