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
        public Func<float> onGetPlayerSpeed = delegate { return 0f; };
        public Func<Transform> onGetLastRescuePerson = delegate { return null; };
        public Func<int> onGetGems = delegate { return 0; };

        public UnityAction<GameObject> onInteractionCollectable = delegate { };
        public UnityAction onPlayerReachBase = delegate { };
        public UnityAction onPlayerLeaveBase = delegate { };

        public UnityAction<GameObject> onPlayerReachTurretAmmoArea = delegate { };
        public UnityAction<Transform> onPlayerInMineArea = delegate { };
        public UnityAction<Transform, Transform> onPlayerInMineAreaLowCapacity = delegate { };
        public UnityAction onPlayerInMilitaryArea = delegate { };
        public UnityAction<Transform> onPlayerInMilitaryAreaLowCapacity = delegate { };


        public UnityAction<SaveLoadStates> onPlayerLeaveBuyArea = delegate { };

        public UnityAction<int> onPlayerSelectGun = delegate { };

        public UnityAction<Transform> onEnemyDie = delegate { };
        public UnityAction onPlayerDie = delegate { };
        public UnityAction onPlayerSpawned = delegate { };


        public UnityAction<bool> onPlayerUseTurret = delegate { };



        public UnityAction<Transform> onRescuePersonAddedToStack = delegate { };


    }
}