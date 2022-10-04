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
        public bool IsTaken = false;

        #endregion

        #region Serialized Variables

        [SerializeField] private float offset = 4f;
        [SerializeField] private float currentOffset = 0f;
        [SerializeField] private GameObject minerPrefab;

        #endregion

        #region Private Variables

        private Vector3 _currentDirection;
        private Transform _playerTransform;
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
            PlayerSignals.Instance.onPlayerInMineArea += OnPlayerInMineArea;
            PlayerSignals.Instance.onPlayerInMineAreaLowCapacity += OnPlayerInMineAreaLowCapacity;
        }

        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerDie -= OnPlayerDisapear;
            PlayerSignals.Instance.onPlayerInMineArea -= OnPlayerInMineArea;
            PlayerSignals.Instance.onPlayerInMineAreaLowCapacity -= OnPlayerInMineAreaLowCapacity;

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
                //do nothing
            }
            else if (State.Equals(RescuePersonState.Run))
            {
                _currentDirection = (_playerTransform.transform.position - transform.position).normalized;
                currentOffset = (_playerTransform.transform.position - transform.position).magnitude;
                if (currentOffset < offset)
                {
                    ChangeState(RescuePersonState.Idle);
                }

                _movementController.ChasePlayer(_currentDirection, _playerTransform);
                _animationController.SetSpeedVariable(_rig.velocity.magnitude);
            }
            else if (State.Equals(RescuePersonState.Idle))
            {
                _movementController.Idle();
                _animationController.SetSpeedVariable(_rig.velocity.magnitude);
                currentOffset = (_playerTransform.transform.position - transform.position).magnitude;
                if (currentOffset > offset)
                {
                    ChangeState(RescuePersonState.Run);
                }
            }
        }

        public void ChangeState(RescuePersonState state)
        {
            State = state;
        }

        public void SetDirection(Transform lookAtObject)
        {
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
        }

        private void OnPlayerInMineArea()
        {
            if (IsTaken)
            {
                Instantiate(minerPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        private void OnPlayerInMineAreaLowCapacity(Transform objTransform)
        {
            if (IsTaken)
            {
                if (transform.Equals(objTransform))
                {
                    Instantiate(minerPrefab, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
    

            }
        }
    }
}