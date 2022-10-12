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
    public class TurretAreaManager : AreaManager
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables



        #endregion

        #endregion
        


        public override void Pay(int value = 1, ScoreTypeEnums scoreType = ScoreTypeEnums.Money)
        {
            UnlockValue -= value;
            UpdateText();
            ScoreSignals.Instance.onScoreDecrease?.Invoke(ScoreTypeEnums.Money, 1);
            if (UnlockValue.Equals(0))
            {
                LevelSignals.Instance.onBuyTurret?.Invoke(AreaID);
                AreaCounts[AreaID] = UnlockValue;

                PlayerSignals.Instance.onPlayerLeaveBuyArea?.Invoke(SaveType, AreaCounts);
                SetDeactive();
            }
        }
    }
}