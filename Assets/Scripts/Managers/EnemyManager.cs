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
    public class EnemyManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public EnemyState State = EnemyState.Walk;

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject[] targets;
        [SerializeField] private EnemyAttackController attackController;
        [SerializeField] private GameObject triggerRange;



        #endregion

        #region Private Variables

        private Material _material;
        private Vector3 _currentTarget;
        private Vector3 _currentDirection;
        private Transform _playerTransform;
        private Transform _defaultTarget;
        private EnemyData _enemyData;
        

        private EnemyMovementController _movementController;


        private Vector3 _dieDirection;


        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _movementController = GetComponent<EnemyMovementController>();
            _enemyData = GetData();
            targets = GameObject.FindGameObjectsWithTag("EnemyTarget");

            SetDefaultTarget();
        }
        private EnemyData GetData() => Resources.Load<CD_Enemy>("Data/CD_Enemy").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            //PlayerSignals.Instance.onPlayerReachBase += OnPlayerDisapear;
        }

        private void UnsubscribeEvents()
        {
            //PlayerSignals.Instance.onPlayerReachBase -= OnPlayerDisapear;


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
            if (State.Equals(EnemyState.Deactive))
            {
                attackController.SetAnimation(EnemyAnimationState.Die);
                _movementController.DeathMove(_dieDirection);
                triggerRange.SetActive(false);
                Destroy(gameObject, _enemyData.DestroyDelay);
            }
            else if (State.Equals(EnemyState.Walk))
            {
                _currentTarget = _defaultTarget.position;
                _currentDirection = (_defaultTarget.position - transform.position).normalized;
                _movementController.MoveToDefaultTarget(_currentDirection, _defaultTarget);
            }
            else if (State.Equals(EnemyState.Run))
            {
                _movementController.ChasePlayer(_currentDirection, _playerTransform);
            }
            
        }

        private void SetDefaultTarget()
        {
            _defaultTarget = targets[UnityEngine.Random.Range(0, targets.Length)].transform;
        }
        public void ChangeState(EnemyState state)
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
            ChangeState(EnemyState.Deactive);
            _dieDirection = dieVector;
        }

        private void OnPlayerDisapear()
        {
            ChangeState(EnemyState.Walk);

        }
    }
}