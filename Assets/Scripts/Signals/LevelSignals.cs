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

        public UnityAction<int> onMinerCountIncreased = delegate { };
        public Func<int> onGetMinerCount = delegate { return 0; };
        public Func<int> onGetMineRemainCapacity = delegate { return 0; };

        public UnityAction<int> onMilitaryPopulationIncreased = delegate { };
        public UnityAction<int> onSoldierCountIncreased = delegate { };
        public Func<int> onGetMilitaryTotalCapacity = delegate { return 0; };
        public Func<int> onGetEmptyReadySoldiersCount = delegate { return 0; };
        public Func<int> onGetSoldierCount = delegate { return 0; };





        public Func<int> onGetCurrentModdedLevel = delegate { return 0; };


    }
}