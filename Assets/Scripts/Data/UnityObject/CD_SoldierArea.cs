using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_SoldierArea", menuName = "Picker3D/CD_SoldierArea", order = 0)]
    public class CD_SoldierArea : ScriptableObject
    {
        public ItemPricesData Data;
    }
}