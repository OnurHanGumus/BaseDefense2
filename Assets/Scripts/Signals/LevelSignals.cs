using Enums;
using Extentions;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class LevelSignals : MonoSingleton<LevelSignals>
    {
        public UnityAction<int> onBuyArea = delegate { };
        public UnityAction<int> onBuyEnemyArea = delegate { };
        public UnityAction<int> onBuyTurret = delegate { };
        public UnityAction<int> onBuyTurretOwners = delegate { };


        public Func<int> onGetCurrentModdedLevel = delegate { return 0; };


    }
}