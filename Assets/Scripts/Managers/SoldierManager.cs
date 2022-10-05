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
        #endregion

        #region Private Variables

        private Material _material;
        private Vector3 _currentDirection;
        private Transform _targetTransform;
        private SoldierData _soldierData;
        private SoldierMovementController _movementController;
        private SoldierAnimationController _animationController;
        



        private Vector3 _dieDirection;


        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _soldierData = GetData();
            _movementController = GetComponent<SoldierMovementController>();
            GetSoldierAreaPosition();
        }



        private SoldierData GetData() => Resources.Load<CD_Soldier>("Data/CD_Soldier").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
        }

        private void UnsubscribeEvents()
        {


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
                Move();
                return;
            }
            if (State.Equals(SoldierStates.Wait))
            {
                _movementController.Idle(); ;
                return;
            }
            if (State.Equals(SoldierStates.GetOutTheBase))
            {
                //do nothing
                return;
            }
            if (State.Equals(SoldierStates.Fight))
            {
                //do nothing
            }


        }

        public void Move()
        {
            if (_targetTransform.Equals(null))
            {
                return;
            }

            _currentDirection = (_targetTransform.position - transform.position).normalized;
            currentOffset = (_targetTransform.transform.position - transform.position).magnitude;

            if (currentOffset < offset)
            {
                ChangeState(SoldierStates.Wait);
            }

            _movementController.MoveToTarget(_currentDirection, _targetTransform);
            //_animationController.SetSpeedVariable(_rig.velocity.magnitude);

        }
        public void ChangeState(SoldierStates state)
        {
            State = state;
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
    }
}