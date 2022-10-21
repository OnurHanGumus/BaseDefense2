using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class PlayerManager2 : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public int CurrentGunId = 0;
        public bool IsPlayerDead = false;
        public bool IsOnBase = true;
        public List<GameObject> Guns;
        public List<Transform> RescuePersonList = new List<Transform>();


        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerAimController2 aimController;
        [SerializeField] private PlayerPhysicsController1 physicsController;


        #endregion

        #region Private Variables
        private PlayerData _data;
        private PlayerMovementController _movementController;
        private PlayerAnimationController _animationController;
        private PlayerRiggingController1 _rigController;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {

            _data = GetData();
            _movementController = GetComponent<PlayerMovementController>();
            _animationController = GetComponent<PlayerAnimationController>();
            _rigController = GetComponent<PlayerRiggingController1>();
        }
        private PlayerData GetData() => Resources.Load<CD_Player>("Data/CD_Player").Data;


        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += _movementController.OnInputDragged;
            InputSignals.Instance.onInputDragged += _animationController.SetSpeedVariable;
            PlayerSignals.Instance.onGetPlayer += OnGetPlayer;
            PlayerSignals.Instance.onPlayerSelectGun += OnGunSelected;
            PlayerSignals.Instance.onEnemyDie += aimController.OnRemoveFromTargetList;
            PlayerSignals.Instance.onPlayerUseTurret += _movementController.OnPlayerUseTurret;
            PlayerSignals.Instance.onPlayerUseTurret += _animationController.OnPlayerUseTurret;
            PlayerSignals.Instance.onRescuePersonAddedToStack += OnRescuePersonAddedToStack;
            PlayerSignals.Instance.onGetPlayerSpeed += _movementController.OnGetPlayerSpeed;
            PlayerSignals.Instance.onGetLastRescuePerson += OnGetLastRescuePerson;
            PlayerSignals.Instance.onPlayerReachNewBase += OnPlayerReachToNewBase;

            SaveSignals.Instance.onInitializeSelectedGunId += OnGunSelected;
            SaveSignals.Instance.onInitializePlayerUpgrades += physicsController.OnGetHealthData;
            SaveSignals.Instance.onUpgradePlayer += physicsController.OnGetHealthLevel;

        }

        private void UnsubscribeEvents()
        {

            InputSignals.Instance.onInputDragged -= _movementController.OnInputDragged;
            InputSignals.Instance.onInputDragged -= _animationController.SetSpeedVariable;
            PlayerSignals.Instance.onGetPlayer -= OnGetPlayer;
            PlayerSignals.Instance.onPlayerSelectGun -= OnGunSelected;
            PlayerSignals.Instance.onEnemyDie -= aimController.OnRemoveFromTargetList;
            PlayerSignals.Instance.onPlayerUseTurret -= _movementController.OnPlayerUseTurret;
            PlayerSignals.Instance.onRescuePersonAddedToStack -= OnRescuePersonAddedToStack;
            PlayerSignals.Instance.onGetPlayerSpeed -= _movementController.OnGetPlayerSpeed;
            PlayerSignals.Instance.onGetLastRescuePerson -= OnGetLastRescuePerson;
            PlayerSignals.Instance.onPlayerReachNewBase -= OnPlayerReachToNewBase;


            SaveSignals.Instance.onInitializeSelectedGunId -= OnGunSelected;
            SaveSignals.Instance.onInitializePlayerUpgrades -= physicsController.OnGetHealthData;
            SaveSignals.Instance.onUpgradePlayer -= physicsController.OnGetHealthLevel;

        }

        public PlayerData GetPlayerData()
        {
            return _data;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private Transform OnGetPlayer()
        {
            return transform;
        }

        public void SetAnimState(PlayerAnimStates state)
        {
            _animationController.SetAnimState(state);
        }

        public void SetAnimBool(PlayerAnimStates state, bool value)
        {
            _animationController.SetAnimBool(state, value);
            _rigController.SetAnimationRig(value, CurrentGunId);
        }

        public void ResetAnimState(PlayerAnimStates state)
        {
            _animationController.ResetAnimState(state);
        }

        public void OnGunSelected(int id)
        {
            CurrentGunId = id;
            aimController.SetGunSettings(Guns[CurrentGunId].transform.GetChild(0));
        }

        private void OnRescuePersonAddedToStack(Transform rescuePerson)
        {
            RescuePersonList.Add(rescuePerson);
        }

        private Transform OnGetLastRescuePerson()
        {

            if (RescuePersonList.Count.Equals(0))
            {
                return transform;
            }
            else
            {
                return RescuePersonList[RescuePersonList.Count - 1];
            }
        }

        private void OnPlayerReachToNewBase()
        {
            RescuePersonList.Clear();
        }



    }
}