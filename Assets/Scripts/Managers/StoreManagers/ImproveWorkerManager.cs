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
using TMPro;
using DG.Tweening;

namespace Managers
{
    public class ImproveWorkerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<TextMeshProUGUI> levelTxt;
        [SerializeField] private List<TextMeshProUGUI> upgradeTxt;
        [SerializeField] private List<int> itemLevels;



        #endregion
        private AllItemPricesData _data;
        #endregion



        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            _data = GetData();
        }
        private AllItemPricesData GetData() => Resources.Load<CD_GunPrices>("Data/StoreBuyPrices/CD_WorkerUpgradePrices").Data;
        private void Start()
        {
            UpdateTexts();

        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onInitializeWorkerUpgrades += OnGetItemLevels;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onInitializeWorkerUpgrades -= OnGetItemLevels;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        public void UpgradeItem(int id)
        {
            itemLevels[id] = itemLevels[id] + 1;
            SaveSignals.Instance.onUpgradeWorker?.Invoke(itemLevels);
            UpdateTexts();
        }

        private void OnGetItemLevels(List<int> levels)
        {

            if (levels.Count.Equals(0))
            {
                levels = new List<int>() { 2, 0};
            }

            itemLevels = levels;
            //UpdateTexts();
        }

        private void UpdateTexts()
        {

            for (int i = 0; i < itemLevels.Count; i++)//textleri initialize et
            {
                levelTxt[i].text = "LEVEL " + (itemLevels[i] + 1).ToString();
                upgradeTxt[i].text =  _data.itemPrices[i].prices[itemLevels[i]].ToString();
            }
        }

        public void CloseBtn()
        {
            UISignals.Instance.onCloseStorePanel?.Invoke(UIPanels.WorkerImprovementsPanel);
        }
    }
}