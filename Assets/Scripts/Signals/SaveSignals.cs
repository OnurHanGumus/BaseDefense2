using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Events;
using Extentions;

namespace Signals
{
    public class SaveSignals: MonoSingleton<SaveSignals>
    {
        public UnityAction<List<int>> onInitializeTurretOwners = delegate { };
        public UnityAction<List<int>> onInitializeEnemyAreas = delegate { };

        public UnityAction<int> onInitializeSetMoney = delegate { };
        public UnityAction<int> onInitializeSetGem = delegate { };
        public UnityAction<SaveLoadStates, int> onSaveCollectables = delegate { };

        public Func<int> onGetSelectedGun = delegate { return 0; };

        public UnityAction<SaveLoadStates,int> onUpgradePlayer = delegate { };

    }
}