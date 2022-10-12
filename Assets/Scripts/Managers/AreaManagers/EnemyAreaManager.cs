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
    public class EnemyAreaManager : AreaManager
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables
        [SerializeField] private GameObject enemyPanel;

        #endregion

        #region Private Variables



        #endregion

        #endregion


        private new void Start()
        {
            base.Start();

            InitializePanel();
        }
        public override void Pay(int value = 1, ScoreTypeEnums scoreType = ScoreTypeEnums.Gem)
        {
            UnlockValue -= value;
            UpdateText();
            ScoreSignals.Instance.onScoreDecrease?.Invoke(ScoreTypeEnums.Gem, 1);
            if (UnlockValue.Equals(0))
            {
                LevelSignals.Instance.onBuyEnemyArea?.Invoke(AreaID);
                AreaCounts[AreaID] = UnlockValue;

                PlayerSignals.Instance.onPlayerLeaveBuyArea?.Invoke(SaveType, AreaCounts);
                SetDeactivePanel();

                //SetDeactive();
            }
        }

        public override Material GetMaterial() => Resources.Load<Material>("Materials/EnemyAreaFloor/1");

        private void InitializePanel()
        {
            if (UnlockValue.Equals(0))
            {
                enemyPanel.SetActive(false);
            }

        }
        private void SetDeactivePanel()
        {
            enemyPanel.SetActive(false);
        }
    }
}