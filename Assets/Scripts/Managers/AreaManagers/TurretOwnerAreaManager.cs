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
using TMPro;

namespace Managers
{
    public class TurretOwnerAreaManager : AreaManager
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject turretOwnerGameObject;
        [SerializeField] private TurretManager turretManager;

        #endregion

        #region Private Variables



        #endregion

        #endregion
 
        private new void Start()
        {
            base.Start();
            
            InitializeOwners();
        }

        public override void Pay(int value = 1, ScoreTypeEnums scoreTypeEnums= ScoreTypeEnums.Money)
        {
            if (turretManager.IsPlayerUsing)
            {
                return;
            }
            UnlockValue -= value;
            UpdateText();
            ScoreSignals.Instance.onScoreDecrease?.Invoke(ScoreTypeEnums.Money, 1);
            if (UnlockValue.Equals(0))
            {
                LevelSignals.Instance.onBuyTurretOwners?.Invoke(AreaID);
                AreaCounts[AreaID] = UnlockValue;

                PlayerSignals.Instance.onPlayerLeaveBuyArea?.Invoke(SaveType, AreaCounts);
                SetActiveOwner();
                turretManager.HasOwner = true;

                SetDeactive();
            }
        }
        private void InitializeOwners()
        {
            if (UnlockValue.Equals(0))
            {
                turretOwnerGameObject.SetActive(true);
                turretManager.HasOwner = true;

            }

        }
        private void SetActiveOwner()
        {
            turretOwnerGameObject.SetActive(true);
        }
    }
}