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

        [SerializeField] private GameObject worker;
        #endregion

        #region Private Variables



        #endregion

        #endregion

        protected new void Start()
        {
            UpdateText();
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
            Instantiate(worker, transform.position, new Quaternion(0,180f,0f,0f));
        }

        public new void UpdateText()
        {
            valueText.text = UnlockValue.ToString();

        }

    }
}