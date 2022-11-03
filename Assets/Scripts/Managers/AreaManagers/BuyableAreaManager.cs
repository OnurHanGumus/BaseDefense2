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
    public class BuyableAreaManager : AreaManager
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables



        #endregion

        #endregion

        private new void Start()
        {
            base.Start();
        }

        public override void Pay(int value = 1, ScoreTypeEnums scoreTypeEnums = ScoreTypeEnums.Money)
        {
            base.Pay();
        }
    }
}