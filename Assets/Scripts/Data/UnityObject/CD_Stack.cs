using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Stack", menuName = "Picker3D/CD_Stack", order = 0)]
    public class CD_Stack : ScriptableObject
    {
        public StackData Data;
    }
}