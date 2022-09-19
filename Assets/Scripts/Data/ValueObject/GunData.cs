using System;

namespace Data.ValueObject
{
    [Serializable]
    public class GunData
    {
        public int GunId = 0;
        public string GunName = "Pistol";
        public int StartDamage = 5;
        public int CurrentDamage = 5;
        public float Delay = 0.5f;
        public int Level = 1;
    }
}