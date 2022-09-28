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

        [SerializeField] private Transform selectedWayObject;
        [SerializeField] private List<Vector3> selectedWay;
        [SerializeField] private List<Transform> childWays;
        [SerializeField] private Transform ammoManager;

        [SerializeField] private List<Transform> waysOnScene; 

        [SerializeField] private List<int> openedTurrets;



        #endregion

        #region Private Variables


        private int _indeks = 0;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            ammoManager = GameObject.FindGameObjectWithTag("CollectAmmoArea").transform;

            waysOnScene.Add(GameObject.FindGameObjectWithTag("0way").transform);
            waysOnScene.Add(GameObject.FindGameObjectWithTag("1way").transform);
            waysOnScene.Add(GameObject.FindGameObjectWithTag("2way").transform);
            waysOnScene.Add(GameObject.FindGameObjectWithTag("3way").transform);
            waysOnScene.Add(GameObject.FindGameObjectWithTag("4way").transform);


        }

        private void Start()
        {
            GoToAmmoManager();
            GetOpenedTurrets();

        }
        private EnemyData GetData() => Resources.Load<CD_Enemy>("Data/CD_Enemy").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onBuyTurret += OnBuyTurrets;
                
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onBuyTurret -= OnBuyTurrets;


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
        private void GoToAmmoManager()
        {
            transform.DOMove(ammoManager.position, 16).SetSpeedBased(true).OnComplete(GoToTurret).SetEase(Ease.Linear);
            transform.DOLookAt(ammoManager.position, 1);
        }
        private void GoToTurret()
        {
            SelectWay();
            
            for (int i = 0; i < selectedWayObject.childCount; i++)
            {
                selectedWay.Add(selectedWayObject.GetChild(i).position);

            }
            transform.DOPath(selectedWay.ToArray(), 16, PathType.Linear, PathMode.Full3D).SetSpeedBased(true).SetEase(Ease.Linear).SetLookAt(0.05f).OnComplete(GoBackToAmmoManager);
        }

        private void GoBackToAmmoManager()
        {

            selectedWay.Reverse();
            transform.DOPath(selectedWay.ToArray(), 16, PathType.Linear, PathMode.Full3D).SetSpeedBased(true).SetEase(Ease.Linear).SetLookAt(0.05f).OnComplete(GoToAmmoManager);

            _indeks++;
            if (_indeks >= openedTurrets.Count)
            {
                _indeks = 0;
            }
        }

        private void SelectWay()
        {
            Debug.Log(_indeks);
            selectedWay.Clear();
            selectedWayObject = waysOnScene[openedTurrets[_indeks] + 1]; //+1 ekliyoruz çünkü oyun baþýnda açýk olan taret numarasýz, diðerleri ise indeks 0'dan baþlayarak kaydediliyor.
        }

        private void OnBuyTurrets(int turretId)
        {
            this.openedTurrets.Add(turretId);
        }

        private void GetOpenedTurrets()
        {
            this.openedTurrets = SaveSignals.Instance.onGetOpenedTurrets();
            openedTurrets.Insert(0, -1);

        }

    }
}