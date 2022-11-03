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
    public abstract class AreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public int AreaID = 1;
        public int[] AreaCounts;
        public SaveLoadStates SaveType;


        #endregion

        #region Serialized Variables
        [SerializeField] protected TextMeshPro valueText;
        [SerializeField] private AreaDataType areaDataType;
        [SerializeField] private MeshRenderer meshRenderer;

        #endregion

        #region Private Variables
        private CurrentLevelAreaData _data;
        private Material _material;

        private int _currentValue;

        public int UnlockValue
        {
            get { return _currentValue; }
            set { _currentValue = value; }
        }

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _data = GetAreaData();
            _material = GetMaterial();
            meshRenderer.material = _material;
            GetDatas();
            
        }
        public CurrentLevelAreaData GetAreaData() => Resources.Load<CD_Area>("Data/Counts/" + areaDataType.ToString()).totalLevelAreaData.Base[LevelSignals.Instance.onGetCurrentModdedLevel()];
        public virtual Material GetMaterial() =>  Resources.Load<Material>("Materials/TurretFloor/"+(LevelSignals.Instance.onGetCurrentModdedLevel() + 1).ToString());

        protected void Start()
        {
            if (UnlockValue.Equals(0))
            {
                SetDeactive();
            }
            UpdateText();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerLeaveBuyArea += OnPlayerLeaveArea;
        }

        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerLeaveBuyArea -= OnPlayerLeaveArea;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        public void UpdateText()
        {
            valueText.text = UnlockValue.ToString();
        }

        public virtual void Pay(int value = 1, ScoreTypeEnums scoreType = ScoreTypeEnums.Money)
        {
            UnlockValue -= value;
            UpdateText();
            ScoreSignals.Instance.onScoreDecrease?.Invoke(scoreType, 1);
            if (UnlockValue.Equals(0))
            {
                LevelSignals.Instance.onBuyArea?.Invoke(AreaID);
                AreaCounts[AreaID] = UnlockValue;

                PlayerSignals.Instance.onPlayerLeaveBuyArea?.Invoke(SaveType, AreaCounts);

                SetDeactive();
            }
        }

        public virtual void SetDeactive()
        {
            gameObject.SetActive(false);
        }

        private void OnPlayerLeaveArea(SaveLoadStates saveState, int[] newArray)
        {
            if (saveState.Equals(SaveType))
            {
                AreaCounts = newArray;
            }
        }

        public void PlayerLeaveArea()
        {
            AreaCounts[AreaID] = UnlockValue;
            PlayerSignals.Instance.onPlayerLeaveBuyArea?.Invoke(SaveType, AreaCounts);
        }

        private void GetDatas()
        {
            AreaCounts = LevelSignals.Instance.onGetAreasCount(SaveType);
            _currentValue = AreaCounts[AreaID];
            if (UnlockValue.Equals(-1))
            {
                AreaCounts = _data.UnlockValues.ToArray();
                _currentValue = AreaCounts[AreaID];
            }
        }
    }
}