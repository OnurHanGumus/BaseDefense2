using System;
using System.Collections.Generic;

namespace Data.ValueObject
{
    [Serializable]
    public class AllGunsData
    {
        public List<GunData> guns = new List<GunData>();
    }
}