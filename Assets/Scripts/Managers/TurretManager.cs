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
using DG.Tweening;

namespace Managers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public bool HasOwner = false;
        public List<Transform> AmmoBoxList = new List<Transform>();


        #endregion

        #region Serialized Variables

        [SerializeField] private TurretRangeController rangeController;

        [SerializeField] private Transform turretOwner;
        #endregion

        #region Private Variables
        private bool _isPlayerUsing = false;
        private float _xValue, _zValue;


        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {

        }
        public Material GetMaterial() => Resources.Load<Material>("Materials/TurretFloor/" + (LevelSignals.Instance.onGetCurrentModdedLevel() + 1).ToString());

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onEnemyDie += rangeController.OnRemoveFromTargetList;
            InputSignals.Instance.onInputDragged += OnInputDragged;
        }

        private void UnsubscribeEvents()
        {

            PlayerSignals.Instance.onEnemyDie -= rangeController.OnRemoveFromTargetList;
            InputSignals.Instance.onInputDragged -= OnInputDragged;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        public void PlayerUseTurret(Transform player)
        {
            _isPlayerUsing = true;
            player.parent = turretOwner;
            //player.transform.DOMove(turretOwner.position, 1f);
            player.transform.position = turretOwner.position;
            player.transform.rotation = turretOwner.rotation;
        }
        public void PlayerLeaveTurret(Transform player)
        {
            _isPlayerUsing = false;

            player.parent = null;
        }

        public void OnInputDragged(InputParams param)
        {
            _xValue = param.XValue;
            _zValue = param.ZValue;
        }

        private void FixedUpdate()
        {
            if (!_isPlayerUsing)
            {
                return;
            }
            Debug.Log(_zValue);

            if (_zValue < -0.9f)
            {
                PlayerSignals.Instance.onPlayerUseTurret?.Invoke(false);
            }
        }
    }
}