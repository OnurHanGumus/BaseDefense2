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
    public class AmmoCollectAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public int WorkerCapacity = 3;

        #endregion

        #region Serialized Variables

        [SerializeField] private MeshRenderer meshRenderer;

        #endregion

        #region Private Variables
        private Material _material;


        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            GetCapacityData();
            _material = GetMaterial();
            meshRenderer.material = _material;
        }
        public Material GetMaterial() => Resources.Load<Material>("Materials/TurretFloor/" + (LevelSignals.Instance.onGetCurrentModdedLevel() + 1).ToString());

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onUpgradeWorker += OnUpgradeWorkerCapacityData;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onUpgradeWorker -= OnUpgradeWorkerCapacityData;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        public void GetCapacityData()
        {
            List<int> upgradeList = SaveSignals.Instance.onGetWorkerUpgrades();
            if (upgradeList.Count < 2)
            {
                upgradeList = new List<int>() { 2, 0 };
            }
            WorkerCapacity = upgradeList[0] + 1;
        }

        public void OnUpgradeWorkerCapacityData(List<int> upgradeList)
        {
            if (upgradeList.Count < 2)
            {
                upgradeList = new List<int>() { 2, 0 };
            }
            WorkerCapacity = upgradeList[0] + 1;
        }

    }
}