using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Extentions;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Enums;
using System.Collections;

namespace Managers
{
    public class RescuePersonManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public RescuePersonState State = RescuePersonState.Terrifie;

        #endregion

        #region Serialized Variables




        #endregion

        #region Private Variables

        private Vector3 _currentTarget;
        private Vector3 _currentDirection;
        private Transform _playerTransform;
        private Transform _defaultTarget;
        private EnemyData _enemyData;



        private RescuePersonMovementController _movementController;
        private RescuePersonAnimationController _animationController;


        private Vector3 _dieDirection;
        private Rigidbody _rig;


        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _rig = GetComponent<Rigidbody>();
            _movementController = GetComponent<RescuePersonMovementController>();
            _animationController = GetComponent<RescuePersonAnimationController>();
            _enemyData = GetData();
        }
        private EnemyData GetData() => Resources.Load<CD_Enemy>("Data/CD_Enemy").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerDie += OnPlayerDisapear;
        }

        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerDie -= OnPlayerDisapear;


        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        public EnemyData GetEnemyData()
        {
            return _enemyData;
        }

        private void FixedUpdate()
        {
            if (State.Equals(RescuePersonState.Terrifie))
            {
                //_movementController.DeathMove(_dieDirection);
                //triggerRange.SetActive(false);
                //Destroy(gameObject, _enemyData.DestroyDelay);
            }
            //else if (State.Equals(RescuePersonState.Run))
            //{
            //    _currentTarget = _defaultTarget.position;
            //    _currentDirection = (_defaultTarget.position - transform.position).normalized;
            //    _movementController.MoveToDefaultTarget(_currentDirection, _defaultTarget);
            //}
            else if (State.Equals(RescuePersonState.Run))
            {
                _movementController.ChasePlayer(_currentDirection, _playerTransform);
                _animationController.SetSpeedVariable(_rig.velocity.magnitude);
            }

        }

        public void ChangeState(RescuePersonState state)
        {
            State = state;
        }

        public void SetDirection(Vector3 direction, Transform lookAtObject)
        {
            _currentDirection = direction;
            _playerTransform = lookAtObject;
        }

        public void DieState(Vector3 dieVector)
        {
            ChangeState(RescuePersonState.Die);
            _dieDirection = dieVector;
        }

        public void ChangeAnim(RescuePersonAnimStates animState)
        {
            _animationController.SetAnimState(animState);
        }


        private void OnPlayerDisapear()
        {
            ChangeState(RescuePersonState.Terrifie);
            //hepsi diz çökecek

        }
    }
}