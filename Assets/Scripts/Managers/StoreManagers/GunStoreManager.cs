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
    public class GunStoreManager : MonoBehaviour
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
        private AllItemPricesData GetData() => Resources.Load<CD_GunPrices>("Data/StoreBuyPrices/CD_GunPrices").Data;
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
            UISignals.Instance.onInitializeGunLevels += OnGetItemLevels;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onInitializeGunLevels -= OnGetItemLevels;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        public void UpgradeItem(int id)
        {
            if (ScoreSignals.Instance.onGetMoney() > _data.itemPrices[id].prices[itemLevels[id]])
            {
                ScoreSignals.Instance.onScoreDecrease?.Invoke(ScoreTypeEnums.Money, _data.itemPrices[id].prices[itemLevels[id]]); //paramýz azaldý

                itemLevels[id] = itemLevels[id] + 1; //item leveli arttý
                UISignals.Instance.onChangeGunLevels?.Invoke(itemLevels);
                PlayerSignals.Instance.onPlayerSelectGun?.Invoke(id);


                UpdateTexts();
            }
            
        }

        public void SelectGun(int id)
        {
            if (itemLevels[id] > 0) //çoktan satýn alýnmýþsa
            {
                PlayerSignals.Instance.onPlayerSelectGun?.Invoke(id);
            }
        }

        private void OnGetItemLevels(List<int> levels)
        {
            if (levels.Count.Equals(0))
            {
                levels = new List<int>() { 1, 0, 0, 0, 0, 0 };
            }

            itemLevels = levels;
            //UpdateTexts();
        }

        private void UpdateTexts()
        {
            for (int i = 0; i < itemLevels.Count; i++)//textleri initialize et
            {
                if (itemLevels[i] == 0)
                {
                    levelTxt[i].text = "LOCKED";
                    upgradeTxt[i].text = "BUY\n" + _data.itemPrices[i].prices[itemLevels[i]];
                }
                else
                {
                    levelTxt[i].text = "LEVEL " + itemLevels[i].ToString();
                    upgradeTxt[i].text = "UPGRADE\n" + _data.itemPrices[i].prices[itemLevels[i]];
                }
            }
        }

        public void CloseBtn()
        {
            UISignals.Instance.onCloseStorePanel?.Invoke(UIPanels.GunStorePanel);
        }



    }
}