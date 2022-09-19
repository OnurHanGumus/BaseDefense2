using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class TotalLevelAreaData
    {
        public List<CurrentLevelAreaData> Base = new List<CurrentLevelAreaData>();
    }
}