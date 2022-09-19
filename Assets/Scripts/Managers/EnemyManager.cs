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


        #endregion

        #region Private Variables

        private Material _material;
        private Vector3 _currentTarget;
        private Vector3 _currentDirection;
        private Transform _playerTransform;
        private Transform _defaultTarget;
        

        private EnemyMovementController _movementController;


        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _movementController = GetComponent<EnemyMovementController>();
            targets = GameObject.FindGameObjectsWithTag("EnemyTarget");

            SetDefaultTarget();
        }
        public Material GetMaterial() => Resources.Load<Material>("Materials/TurretFloor/" + (LevelSignals.Instance.onGetCurrentModdedLevel() + 1).ToString());

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

        private void FixedUpdate()
        {
            if (State.Equals(EnemyState.Walk))
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




    }
}