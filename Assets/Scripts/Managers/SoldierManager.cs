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
    public class SoldierManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public SoldierStates State = SoldierStates.Init;
        public SoldierAnimStates AnimState = SoldierAnimStates.Run;

        #endregion

        #region Serialized Variables

        [SerializeField] private float offset = 0.5f;
        [SerializeField] private float currentOffset = 0f;
        [SerializeField] private List<Transform> getOutBasePoints;
        [SerializeField] private SoldierAimController rangeController;
        [SerializeField] private SoldierShootRangeTrigger shootrangeTrigger;

        #endregion

        #region Private Variables

        private Material _material;
        private Vector3 _currentDirection;
        private Transform _targetTransform;
        private SoldierData _soldierData;
        private SoldierMovementController _movementController;
        private SoldierAnimationController _animationController;
        private Rigidbody _rig;
        private Transform _wayTransform;

        private bool _isThereNearEnemy = false;


        private Vector3 _dieDirection;


        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _wayTransform = GameObject.FindGameObjectWithTag("SoldierWay").transform;
            SetWays();
            _soldierData = GetData();
            _movementController = GetComponent<SoldierMovementController>();
            _animationController = GetComponent<SoldierAnimationController>();
            _rig = GetComponent<Rigidbody>();
        }

        

        private SoldierData GetData() => Resources.Load<CD_Soldier>("Data/CD_Soldier").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onEnemyDie += rangeController.OnRemoveFromTargetList;
            PlayerSignals.Instance.onEnemyDie += shootrangeTrigger.OnRemoveFromTargetList;
            SoldierSignals.Instance.onSoldierAttack += OnSoldierAttack;
        }

        private void UnsubscribeEvents()
        {

            PlayerSignals.Instance.onEnemyDie -= rangeController.OnRemoveFromTargetList;
            PlayerSignals.Instance.onEnemyDie -= shootrangeTrigger.OnRemoveFromTargetList;
            SoldierSignals.Instance.onSoldierAttack -= OnSoldierAttack;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        public SoldierData GetSoldierData()
        {
            return _soldierData;
        }

        private void FixedUpdate()
        {
            if (State.Equals(SoldierStates.Init))
            {
                Move(_targetTransform, SoldierStates.Wait, 0.5f);
                return;
            }
            if (State.Equals(SoldierStates.Wait))
            {
                _movementController.Idle();
                _animationController.SetSpeedVariable(_rig.velocity.magnitude);
                return;
            }
            if (State.Equals(SoldierStates.GetOutTheBase0))
            {
                Move(getOutBasePoints[0], SoldierStates.GetOutTheBase1, 0.5f);
                return;
            }
            if (State.Equals(SoldierStates.GetOutTheBase1))
            {
                Move(getOutBasePoints[1], SoldierStates.Fight, 0.5f);
                return;
            }
            if (State.Equals(SoldierStates.Fight))
            {
                if (shootrangeTrigger.IsEnemyNear)
                {
                    _movementController.Aim(shootrangeTrigger.TargetList[0]);
                    _animationController.SetSpeedVariable(_rig.velocity.magnitude);
                }
                else
                {
                    if (rangeController.TargetList.Count <= 0) //çevrede düþman yok collideri büyüt
                    {
                        _movementController.Idle();
                        _animationController.SetSpeedVariable(_rig.velocity.magnitude);
                        rangeController.SearchEnemy();
                    }
                    else//bulduðun düþmana yaklaþ
                    {

                        Move(rangeController.TargetList[0], SoldierStates.Fight, 2f);
                    }
                } 
            }
        }

        private void Start()
        {
            GetSoldierAreaPosition();

        }
        private void SetWays()
        {
            for (int i = 0; i < _wayTransform.childCount; i++)
            {
                getOutBasePoints.Add(_wayTransform.GetChild(i));
            }
        }
        public void Move(Transform target, SoldierStates newState, float offset)
        {
            if (target == null)
            {
                return;
            }

            _currentDirection = (target.position - transform.position).normalized;
            currentOffset = (target.transform.position - transform.position).magnitude;

            if (currentOffset < offset)
            {
                ChangeState(newState);
                return;
            }

            _movementController.MoveToTarget(_currentDirection, target);
            _animationController.SetSpeedVariable(_rig.velocity.magnitude);

        }
        public void ChangeState(SoldierStates state)
        {
            State = state;
        }

        public void ChangeAnimState(SoldierAnimStates state)
        {
            _animationController.SetAnimState(state);
        }

        public void SetDirection(Transform lookAtObject)
        {
            _targetTransform = lookAtObject;
        }

        public void DieState(Vector3 dieVector)
        {
            ChangeState(SoldierStates.Die);
            _dieDirection = dieVector;
        }

        private void GetSoldierAreaPosition()
        {
            SetDirection(SoldierSignals.Instance.onGetSoldierAreaTransform());
        }

        private void OnSoldierAttack()
        {
            ChangeState(SoldierStates.GetOutTheBase0);
        }
    }
}