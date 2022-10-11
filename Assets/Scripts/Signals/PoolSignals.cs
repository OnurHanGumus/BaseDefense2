using Enums;
using Extentions;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        public Func<GameObject> onGetEnemyFromPool = delegate { return null; };
        public Func<GameObject> onGetGemFromPool = delegate { return null; };
        public Func<GameObject> onGetMoneyFromPool = delegate { return null; };
        public Func<Transform> onGetPoolManagerObj = delegate { return null; };


    }
}