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
                Move(_targetTransform, SoldierStates.Wait);
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
                Move(getOutBasePoints[0], SoldierStates.GetOutTheBase1);
                return;
            }
            if (State.Equals(SoldierStates.GetOutTheBase1))
            {
                Move(getOutBasePoints[1], SoldierStates.Wait);
                return;
            }
            if (State.Equals(SoldierStates.Fight))
            {
                //do nothing
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
        public void Move(Transform target, SoldierStates newState)
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
    }
}