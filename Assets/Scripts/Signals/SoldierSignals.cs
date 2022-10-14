using Enums;
using Extentions;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class SoldierSignals : MonoSingleton<SoldierSignals>
    {
        public UnityAction<Transform, Transform> onBecomeSoldier = delegate { };

        public Func<Transform> onGetSoldierAreaTransform = delegate { return null; };
        public Func<int> onGetReadySoldierAreaTotalCount = delegate { return 0; };

        public UnityAction onSoldierAttack = delegate { };
    }
}