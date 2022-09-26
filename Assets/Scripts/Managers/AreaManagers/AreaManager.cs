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
    public class AreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public int AreaID = 1;


        #endregion

        #region Serialized Variables
        [SerializeField] protected TextMeshPro valueText;
        [SerializeField] private AreaDataType areaDataType;
        [SerializeField] private MeshRenderer meshRenderer;

        #endregion

        #region Private Variables
        private CurrentLevelAreaData _data;
        private Material _material;

        public int UnlockValue
        {
            get { return _data.UnlockValues[AreaID]; }
            set { _data.UnlockValues[AreaID] = value; }
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
            //LevelSignals.Instance.onBuyEnemyArea += OnBuy;
        }

        private void UnsubscribeEvents()
        {
            //LevelSignals.Instance.onBuyEnemyArea -= OnBuy;

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

        public virtual void Pay(int value = 1)
        {
            UnlockValue -= value;
            UpdateText();
            ScoreSignals.Instance.onScoreDecrease?.Invoke(ScoreTypeEnums.Money, 1);
            if (UnlockValue.Equals(0))
            {
                LevelSignals.Instance.onBuyArea?.Invoke(AreaID);
                SetDeactive();
            }
        }

        public virtual void SetDeactive()
        {
            gameObject.SetActive(false);
        }


    }
}