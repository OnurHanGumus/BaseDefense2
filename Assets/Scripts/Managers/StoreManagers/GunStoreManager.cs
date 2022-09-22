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

namespace Managers
{
    public class GunStoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<TextMeshProUGUI> levelTxt;
        [SerializeField] private List<int> itemLevels;
        [SerializeField] private int currentSelectedGun;


        #endregion
        private GunData _data;
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

        private void UpgradeItem()
        {

        }

        private void BuyGun(int id)
        {
            
        }
        private void SelectGun(int id)
        {

        }

        private void OnGetItemLevels(List<int> levels)
        {
            if (levels.Count.Equals(0))
            {
                levels = new List<int>() { 0, 0, 0, 0, 0, 0 };
            }

            itemLevels = levels;
            Debug.Log(levels.Count);
            for (int i = 0; i < levels.Count; i++)
            {
                levelTxt[i].text = "LEVEL " + levels[i].ToString();
            }
        }



    }
}