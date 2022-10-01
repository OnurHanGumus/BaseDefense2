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
        private int _speed = 1;
        private int _indeks = 0;
        private List<int> _openedTurrets = new List<int>();
        private List<Transform> _waysOnScene = new List<Transform>();
        private List<Vector3> _selectedWay = new List<Vector3>();

        private Transform _selectedWayObject;
        private Transform _ammoManager;


        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _ammoManager = GameObject.FindGameObjectWithTag("CollectAmmoArea").transform;

            _waysOnScene.Add(GameObject.FindGameObjectWithTag("0way").transform);
            _waysOnScene.Add(GameObject.FindGameObjectWithTag("1way").transform);
            _waysOnScene.Add(GameObject.FindGameObjectWithTag("2way").transform);
            _waysOnScene.Add(GameObject.FindGameObjectWithTag("3way").transform);
            _waysOnScene.Add(GameObject.FindGameObjectWithTag("4way").transform);


        }

        private void Start()
        {
            GetSpeedData();

            GoToAmmoManager();
            GetOpenedTurrets();

        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onUpgradeWorker += OnUpgradeWorkerSpeedData;
            LevelSignals.Instance.onBuyTurret += OnBuyTurrets;

        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onUpgradeWorker -= OnUpgradeWorkerSpeedData;
            LevelSignals.Instance.onBuyTurret -= OnBuyTurrets;


        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void GoToAmmoManager()
        {
            transform.DOMove(_ammoManager.position, 4 * _speed).SetSpeedBased(true).OnComplete(GoToTurret).SetEase(Ease.Linear);
            transform.DOLookAt(_ammoManager.position, 1);
        }
        private void GoToTurret()
        {
            SelectWay();
            
            for (int i = 0; i < _selectedWayObject.childCount; i++)
            {
                _selectedWay.Add(_selectedWayObject.GetChild(i).position);

            }
            transform.DOPath(_selectedWay.ToArray(), 4 * _speed, PathType.Linear, PathMode.Full3D).SetSpeedBased(true).SetEase(Ease.Linear).SetLookAt(0.05f).OnComplete(GoBackToAmmoManager);
        }

        private void GoBackToAmmoManager()
        {

            _selectedWay.Reverse();
            transform.DOPath(_selectedWay.ToArray(), 4 * _speed, PathType.Linear, PathMode.Full3D).SetSpeedBased(true).SetEase(Ease.Linear).SetLookAt(0.05f).OnComplete(GoToAmmoManager);

            _indeks++;
            if (_indeks >= _openedTurrets.Count)
            {
                _indeks = 0;
            }
        }

        private void SelectWay()
        {
            _selectedWay.Clear();
            _selectedWayObject = _waysOnScene[_openedTurrets[_indeks] + 1]; //+1 ekliyoruz çünkü oyun baþýnda açýk olan taret numarasýz, diðerleri ise indeks 0'dan baþlayarak kaydediliyor.
        }

        private void OnBuyTurrets(int turretId)
        {
            this._openedTurrets.Add(turretId);
        }

        private void GetOpenedTurrets()
        {
            this._openedTurrets = SaveSignals.Instance.onGetOpenedTurrets();
            _openedTurrets.Insert(0, -1);

        }
        public void GetSpeedData()
        {
            List<int> upgradeList = SaveSignals.Instance.onGetWorkerUpgrades();
            if (upgradeList.Count < 2)
            {
                upgradeList = new List<int>() { 2, 0 };
            }
            _speed = upgradeList[1] + 1;
        }
        public void OnUpgradeWorkerSpeedData(List<int> upgradeList)
        {
            if (upgradeList.Count < 2)
            {
                upgradeList = new List<int>() { 2, 0 };
            }
            _speed = upgradeList[1] + 1;
        }

    }
}