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
        [SerializeField] private EnemyPhysicsController physicsController;
        [SerializeField] private GameObject triggerRange;
        [SerializeField] private GameObject moneyPrefab;



        #endregion

        #region Private Variables

        private Material _material;
        private Vector3 _currentTarget;
        private Vector3 _currentDirection;
        public Transform _playerTransform;
        private Transform _defaultTarget;
        private EnemyData _enemyData;
        

        private EnemyMovementController _movementController;
        private EnemyAnimationController _animationController;


        private Vector3 _dieDirection;
        private bool _isMoneyInstantiated = false;

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

            _animationController = GetComponent<EnemyAnimationController>();
            SetDefaultTarget();
        }
        private EnemyData GetData() => Resources.Load<CD_Enemy>("Data/CD_Enemy").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
            State = EnemyState.Walk;
            triggerRange.SetActive(true);
            SetDefaultTarget();
            _isMoneyInstantiated = false;

        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerReachBase += OnPlayerDisapear;
            PlayerSignals.Instance.onPlayerSelectGun += physicsController.OnPlayerChangeGun;
            SoldierSignals.Instance.onSoldierDeath += OnSoldierDisapear;

            LevelSignals.Instance.onBossDefeated += OnBossDefeated;

        }

        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerReachBase -= OnPlayerDisapear;
            PlayerSignals.Instance.onPlayerSelectGun -= physicsController.OnPlayerChangeGun;
            SoldierSignals.Instance.onSoldierDeath -= OnSoldierDisapear;

            LevelSignals.Instance.onBossDefeated -= OnBossDefeated;

        }


        private void OnDisable()
        {
            UnsubscribeEvents();
            State = EnemyState.Walk;

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
                _movementController.Deactive(_playerTransform);
            }
            else if (State.Equals(EnemyState.Die))
            {
                _animationController.SetAnimation(EnemyAnimationState.Die);
                _movementController.DeathMove(_dieDirection);
                triggerRange.SetActive(false);

                physicsController.ResetData();
                StartCoroutine(DeactivateEnemy());
            }
            else if (State.Equals(EnemyState.Walk))
            {
                _currentTarget = _defaultTarget.position;
                _currentDirection = (_defaultTarget.position - transform.position).normalized;
                _movementController.MoveToDefaultTarget(_currentDirection, _defaultTarget);
            }
            else if (State.Equals(EnemyState.Run))
            {
                if (_playerTransform == null)
                {
                    ChangeState(EnemyState.Walk);
                }
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
            ChangeState(EnemyState.Die);
            _dieDirection = dieVector;
        }

        private void OnPlayerDisapear()
        {
            ChangeState(EnemyState.Walk);

        }
        private void OnSoldierDisapear(Transform soldierTransform)
        {
            if (_playerTransform == soldierTransform)
            {
                ChangeState(EnemyState.Walk);
                ChangeAnimState(EnemyAnimationState.Walk);
            }

        }
        private IEnumerator DeactivateEnemy()
        {
            InstantiateMoneys();
            yield return new WaitForSeconds(_enemyData.DestroyDelay);
            PlayerSignals.Instance.onEnemyDie?.Invoke(transform);
            gameObject.SetActive(false);
        }

        private void InstantiateMoneys()
        {
            if (!_isMoneyInstantiated)
            {
                for (int i = 0; i < 3; i++)
                {
                    //Instantiate(moneyPrefab, transform.position, transform.rotation);
                    GameObject tmp = PoolSignals.Instance.onGetMoneyFromPool();
                    if (tmp == null)
                    {
                        tmp = Instantiate(moneyPrefab, transform.position, transform.rotation);
                    }
                    tmp.transform.position = transform.position;
                    tmp.transform.rotation = transform.rotation;
                    tmp.SetActive(true);

                }
                _isMoneyInstantiated = true;
            }
        }

        public void ChangeAnimState(EnemyAnimationState state)
        {
            _animationController.SetAnimation(state);
        }

        private void OnBossDefeated()
        {
            ChangeState(EnemyState.Die);
        }
    }
}