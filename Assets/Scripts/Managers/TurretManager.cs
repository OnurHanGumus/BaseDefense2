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


        #endregion

        #region Serialized Variables
        [SerializeField] private List<int> turretOwnersList;
        [SerializeField] private GameObject turretOwnerGameObject;
        [SerializeField] private int turretId = 1;
        [SerializeField] private bool hasOwner = false;
        [SerializeField] private List<MeshRenderer> meshList;


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
            _material = GetMaterial();

            foreach (var i in meshList)
            {
                i.material = _material;
            }
        }
        public Material GetMaterial() => Resources.Load<Material>("Materials/TurretFloor/" + (LevelSignals.Instance.onGetCurrentModdedLevel() + 1).ToString());

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onInitializeTurretOwners += OnSetTurretOwners;
            LevelSignals.Instance.onBuyTurretOwners += OnSetActiveOwner;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onInitializeTurretOwners -= OnSetTurretOwners;
            LevelSignals.Instance.onBuyTurretOwners -= OnSetActiveOwner;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnSetTurretOwners(List<int> ownersList)
        {
            turretOwnersList = ownersList;

            foreach (var i in ownersList)
            {
                if (i.Equals(turretId))
                {
                    hasOwner = true;
                    turretOwnerGameObject.SetActive(true);
                }
            }
        }

        private void OnSetActiveOwner(int id)
        {
            if (turretId.Equals(id))
            {
                turretOwnerGameObject.SetActive(true);
            }
        }
    }
}