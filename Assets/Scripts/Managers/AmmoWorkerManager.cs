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
using System.Collections;
using DG.Tweening;

namespace Managers
{
    public class AmmoWorkerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables





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
        private EnemyData GetData() => Resources.Load<CD_Enemy>("Data/CD_Enemy").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {

        }

        private void UnsubscribeEvents()
        {


        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        //public EnemyData GetEnemyData()
        //{
        //    return _enemyData;
        //}

    }
}