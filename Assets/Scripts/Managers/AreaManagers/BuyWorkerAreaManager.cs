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
    public class BuyWorkerAreaManager : AreaManager
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables


        #endregion

        #region Private Variables



        #endregion

        #endregion

        protected new void Start()
        {
            if (UnlockValue.Equals(0))
            {
                InstantiateWorker();
                SetDeactive();
            }
        }

        public override void Pay(int value = 1)
        {
            UnlockValue -= value;
            UpdateText();
            ScoreSignals.Instance.onScoreDecrease?.Invoke(ScoreTypeEnums.Money, 1);
            if (UnlockValue.Equals(0))
            {
                //SaveSignals.Instance.onIncreaseAmmoWorkerCount?.Invoke(SaveLoadStates.AmmoWorkerCounts);
                InstantiateWorker();
                SetDeactive();
            }
        }

        public override void SetDeactive()
        {
            base.valueText.text = "FREE ";
        }

        public void InstantiateWorker()
        {
            Instantiate(new GameObject());
        }
    }
}