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
        public UnityAction<Transform> onAddEnemyToPool = delegate { };


    }
}