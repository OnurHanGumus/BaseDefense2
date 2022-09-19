using Extentions;
using Keys;
using System;
using UnityEngine;
using UnityEngine.Events;
using Enums;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public Func<Transform> onGetPlayer = delegate { return null;  };
        public UnityAction<GameObject> onInteractionCollectable = delegate { };
        public UnityAction onPlayerReachBase = delegate { };
        public UnityAction onPlayerLeaveBase = delegate { };

        public UnityAction<SaveLoadStates> onPlayerLeaveBuyArea = delegate { };

        public UnityAction<int> onPlayerSelectGun = delegate { };
    }
}