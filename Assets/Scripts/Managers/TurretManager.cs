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
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public bool HasOwner = false;
        public List<Transform> AmmoBoxList = new List<Transform>();


        #endregion

        #region Serialized Variables

        [SerializeField] private TurretRangeController rangeController;

        #endregion

        #region Private Variables



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
        }

        private void UnsubscribeEvents()
        {

            PlayerSignals.Instance.onEnemyDie -= rangeController.OnRemoveFromTargetList;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


    }
}