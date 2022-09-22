using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;
namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_GunPrices", menuName = "Picker3D/CD_GunPrices", order = 0)]
    public class CD_GunPrices : ScriptableObject
    {
        public AllItemPricesData Data;

    }
}