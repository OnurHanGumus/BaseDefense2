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
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables




        #endregion

        #region Private Variables
        private PlayerData _data;
        private PlayerMovementController _movementController;
        private PlayerAnimationController _animationController;
        private PlayerRiggingController _rigController;


        #endregion

        private int _currentGunId = 0;
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
 
            _data = GetPlayerData();
            _movementController = GetComponent<PlayerMovementController>();
            _animationController = GetComponent<PlayerAnimationController>();
            _rigController = GetComponent<PlayerRiggingController>();
        }
        public PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;
        

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
        }

        private void UnsubscribeEvents()
        {

            InputSignals.Instance.onInputDragged -= _movementController.OnInputDragged;
            InputSignals.Instance.onInputDragged -= _animationController.SetSpeedVariable;
            PlayerSignals.Instance.onGetPlayer -= OnGetPlayer;
            PlayerSignals.Instance.onPlayerSelectGun -= OnGunSelected;



        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            _currentGunId = SaveSignals.Instance.onGetSelectedGun();

        }

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
            _rigController.SetAnimationRig(value, _currentGunId);
        }

        public void ResetAnimState(PlayerAnimStates state)
        {
            _animationController.ResetAnimState(state);
        }

        public void OnGunSelected(int id)
        {
            _currentGunId = id;
        }



    }
}