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
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables
        [SerializeField] private IncreaseCommand increaseCommand;


        #endregion

        #region Private Variables
        private int _money = 0;
        private int _gem = 0;

        [ShowInInspector]
        public int Money
        {
            get { return _money; }
            set { 
                _money = value;

                }
        }
        [ShowInInspector]

        public int Gem
        {
            get { return _gem; }
            set { _gem = value; }
        }



        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {

        }
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onScoreIncrease += OnScoreIncrease;
            ScoreSignals.Instance.onScoreDecrease += OnScoreDecrease;
            ScoreSignals.Instance.onGetMoney += OnGetMoney;
            ScoreSignals.Instance.onGetGem += OnGetGem;

            SaveSignals.Instance.onInitializeSetMoney += OnInitializeSetMoney;
            SaveSignals.Instance.onInitializeSetGem += OnInitializeSetGem;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onScoreIncrease -= OnScoreIncrease;
            ScoreSignals.Instance.onScoreDecrease -= OnScoreDecrease;
            ScoreSignals.Instance.onGetMoney -= OnGetMoney;
            ScoreSignals.Instance.onGetGem -= OnGetGem;
            SaveSignals.Instance.onInitializeSetMoney -= OnInitializeSetMoney;
            SaveSignals.Instance.onInitializeSetGem -= OnInitializeSetGem;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnScoreIncrease(ScoreTypeEnums type, int amount)
        {
            if (type.Equals(ScoreTypeEnums.Money))
            {
                _money += amount;
                UISignals.Instance.onSetChangedText?.Invoke(type, _money);
                SaveSignals.Instance.onSaveCollectables?.Invoke(SaveLoadStates.Money, _money);

            }
            else
            {
                _gem += amount;
                UISignals.Instance.onSetChangedText?.Invoke(type, _gem);
                SaveSignals.Instance.onSaveCollectables?.Invoke(SaveLoadStates.Gem, _gem);
            }
        }

        private void OnScoreDecrease(ScoreTypeEnums type, int amount)
        {
            if (type.Equals(ScoreTypeEnums.Money))
            {
                _money -= amount;
                UISignals.Instance.onSetChangedText?.Invoke(type, _money);
                SaveSignals.Instance.onSaveCollectables?.Invoke(SaveLoadStates.Money, _money);

            }
            else
            {
                _gem -= amount;
                UISignals.Instance.onSetChangedText?.Invoke(type, _gem);
                SaveSignals.Instance.onSaveCollectables?.Invoke(SaveLoadStates.Gem, _gem);
            }
        }

        private int OnGetMoney()
        {
            return Money;
        }

        private int OnGetGem()
        {
            return Gem;
        }

        private void OnInitializeSetMoney(int amount)
        {
            Money = amount;
            UISignals.Instance.onSetChangedText?.Invoke(ScoreTypeEnums.Money, _money);

        }

        private void OnInitializeSetGem(int amount)
        {
            Gem = amount;
            UISignals.Instance.onSetChangedText?.Invoke(ScoreTypeEnums.Gem, _gem);

        }
    }
}