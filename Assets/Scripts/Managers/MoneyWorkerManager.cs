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
        [SerializeField] private Transform releaseMoneyPos;


        [SerializeField] private SphereCollider sphereCollider;
        [SerializeField] private BoxCollider physicsCollider;
        [SerializeField] private WorkerStackManager stackManager;
        [SerializeField] private MoneyWorkerRangeController rangeController;

        [SerializeField] private Vector3 lastPositionBeforeBase;


        #endregion

        #region Private Variables
        private int _speed = 1;
        private int _capacity = 3;
        private WorkerAnimationController _animationController;
        private List<Vector3> _selectedWay = new List<Vector3>();

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            initWayObj = GameObject.FindGameObjectWithTag("MoneyWay").transform;
            _animationController = GetComponent<WorkerAnimationController>();
        }

        private void Start()
        {
            GetData();

            InitMove();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onUpgradeWorker += OnUpgradeWorkerData;
            StackSignals.Instance.onMoneyWorkerCollectMoney += rangeController.OnMoneyOnListCollected;
            PlayerSignals.Instance.onInteractionCollectable += rangeController.OnPlayerCollectedMoney;

        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onUpgradeWorker -= OnUpgradeWorkerData;
            StackSignals.Instance.onMoneyWorkerCollectMoney -= rangeController.OnMoneyOnListCollected;
            PlayerSignals.Instance.onInteractionCollectable -= rangeController.OnPlayerCollectedMoney;


        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void InitMove()
        {
            _selectedWay.Clear();
            for (int i = 0; i < initWayObj.childCount; i++)
            {
                _selectedWay.Add(initWayObj.GetChild(i).position);

            }
            transform.DOPath(_selectedWay.ToArray(), 4 * _speed, PathType.Linear, PathMode.Full3D).SetSpeedBased(true).SetEase(Ease.Linear).SetLookAt(0.05f).OnComplete(StartSearchForMoney);
        }

        private void StartSearchForMoney()
        {
            StartCoroutine(SearchForMoney());
        }

        private IEnumerator SearchForMoney()
        {
            
            if (rangeController.MoneyList.Count > 0)
            {
                physicsCollider.enabled = false;
                yield return new WaitForSeconds(0.05f);
                physicsCollider.enabled = true;
                _animationController.SetAnimationTrigger(WorkerAnimStates.Walk);

                MoveToMoney();
                StopAllCoroutines();
                
            }
            else
            {
                if (sphereCollider.radius < 20)
                {
                    sphereCollider.radius += 0.1f;
                }
                _animationController.SetAnimationTrigger(WorkerAnimStates.Idle);
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
            if (stackManager.CollectableStack.Count >= _capacity)
            {
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
            _selectedWay.Clear();
            for (int i = 0; i < initWayObj.childCount; i++)
            {
                _selectedWay.Add(initWayObj.GetChild(i).position);

            }
            _selectedWay.Reverse();
            _selectedWay.RemoveAt(_selectedWay.Count - 1);
            _selectedWay.Add(_selectedWay[0]);
            transform.DOPath(_selectedWay.ToArray(), 4 * _speed, PathType.Linear, PathMode.Full3D).SetSpeedBased(true).SetEase(Ease.Linear).SetLookAt(0.05f).OnComplete(GoToSearchArea);
        }

        private void GoToSearchArea()
        {
            sphereCollider.radius = 1f;
            rangeController.MoneyList.Clear();
            transform.DOMove(lastPositionBeforeBase, 4 * _speed).SetSpeedBased(true).OnComplete(CheckCapacity).SetEase(Ease.Linear);
            transform.DOLookAt(lastPositionBeforeBase, 1);
        }




        public void GetData()
        {
            List<int> upgradeList = SaveSignals.Instance.onGetWorkerUpgrades();
            if (upgradeList.Count < 2)
            {
                upgradeList = new List<int>() { 2, 0 };
            }
            _capacity = upgradeList[0] + 1;
            _speed = upgradeList[1] + 1;
        }

        public void OnUpgradeWorkerData(List<int> upgradeList)
        {
            if (upgradeList.Count < 2)
            {
                upgradeList = new List<int>() { 2, 0 };
            }
            _capacity = upgradeList[0] + 1;
            _speed = upgradeList[1] + 1;
        }
    }
}