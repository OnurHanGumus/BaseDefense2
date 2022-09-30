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
    public class MoneyWorkerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private Transform initWayObj;
        [SerializeField] private List<Vector3> selectedWay;
        [SerializeField] private Transform releaseMoneyPos;


        [SerializeField] private SphereCollider sphereCollider;
        [SerializeField] private WorkerStackManager stackManager;
        [SerializeField] private MoneyWorkerRangeController rangeController;

        [SerializeField] private Vector3 lastPositionBeforeBase;
        Sequence sequence;


        #endregion

        #region Private Variables
        private int _speed = 1;
        private int _indeks = 0;


        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            initWayObj = GameObject.FindGameObjectWithTag("MoneyWay").transform;
            sequence = DOTween.Sequence();

        }

        private void Start()
        {
            GetSpeedData();

            InitMove();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onUpgradeWorker += OnUpgradeWorkerSpeedData;
            StackSignals.Instance.onMoneyWorkerCollectMoney += rangeController.OnMoneyOnListCollected;

        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onUpgradeWorker -= OnUpgradeWorkerSpeedData;
            StackSignals.Instance.onMoneyWorkerCollectMoney -= rangeController.OnMoneyOnListCollected;


        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void InitMove()
        {
            selectedWay.Clear();
            for (int i = 0; i < initWayObj.childCount; i++)
            {
                selectedWay.Add(initWayObj.GetChild(i).position);

            }
            transform.DOPath(selectedWay.ToArray(), 4 * _speed, PathType.Linear, PathMode.Full3D).SetSpeedBased(true).SetEase(Ease.Linear).SetLookAt(0.05f).OnComplete(StartSearchForMoney);
        }

        private void StartSearchForMoney()
        {
            StartCoroutine(SearchForMoney());
        }

        private IEnumerator SearchForMoney()
        {
            
            if (rangeController.MoneyList.Count > 0)
            {
                Debug.Log("range miktari 0'dan büyük");

                MoveToMoney();
                StopAllCoroutines();
                
            }
            else
            {
                Debug.Log("büyüyor");

                sphereCollider.radius += 0.1f;
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(SearchForMoney());
            }
 
        }

        private void MoveToMoney()
        {
            transform.DOMove(rangeController.MoneyList[0].position, 4 * _speed).SetSpeedBased(true).OnComplete(CheckCapacity).SetEase(Ease.Linear);
            transform.DOLookAt(rangeController.MoneyList[0].position, 1);
        }

        private void CheckCapacity()
        {
            if (stackManager.CollectableStack.Count >= 3)
            {
                Debug.Log("stackmanager sayýsý 3ü geçti");
                GoBackToBase();
            }
            else
            {
                StartCoroutine(SearchForMoney());
            }
        }

        private void GoBackToBase()
        {
            
            lastPositionBeforeBase = transform.position;
            selectedWay.Clear();
            for (int i = 0; i < initWayObj.childCount; i++)
            {
                selectedWay.Add(initWayObj.GetChild(i).position);

            }
            selectedWay.Reverse();
            selectedWay.RemoveAt(selectedWay.Count - 1);
            transform.DOPath(selectedWay.ToArray(), 4 * _speed, PathType.Linear, PathMode.Full3D).SetSpeedBased(true).SetEase(Ease.Linear).SetLookAt(0.05f).OnComplete(GoToSearchArea);
        }

        private void GoToSearchArea()
        {
            transform.DOMove(lastPositionBeforeBase, 4 * _speed).SetSpeedBased(true).OnComplete(CheckCapacity).SetEase(Ease.Linear);
            transform.DOLookAt(initWayObj.GetChild(2).position, 1);
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