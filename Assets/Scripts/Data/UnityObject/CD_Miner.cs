using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Miner", menuName = "Picker3D/CD_Miner", order = 0)]
    public class CD_Miner : ScriptableObject
    {
        public ItemPricesData Data;
    }
}