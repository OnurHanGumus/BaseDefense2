using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Signals;
using Data.UnityObject;
using Data.ValueObject;
using Commands;
using DG.Tweening;
using Enums;
using System.Collections;
using Random = UnityEngine.Random;

namespace Managers
{
    public class WorkerStackManager : MonoBehaviour
    {
        #region Self Variables
        public int Capacity = 3;
        #region Public Variables
        public List<GameObject> CollectableStack = new List<GameObject>();
        public List<GameObject> Temp = new List<GameObject>();


        #endregion

        #region Seralized Veriables
        #endregion

        #region Private Variables
        
        private StackData _stackData;


        #endregion
        #endregion
        
        private void Awake()
        {
            _stackData = GetStackData();
            Init();
        }

        private StackData GetStackData() => Resources.Load<CD_Stack>("Data/CD_Stack").Data;

        private void Init()
        {
            GetCapacityData();
        }

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            CoreGameSignals.Instance.onReset += OnReset; 
            SaveSignals.Instance.onUpgradeWorker += OnUpgradeWorkerCapacityData;

        }
        private void UnSubscribeEvent()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            SaveSignals.Instance.onUpgradeWorker -= OnUpgradeWorkerCapacityData;

        }
        private void OnDisable()
        {
            UnSubscribeEvent();
        }
        #endregion

        public void InteractionWithCollectable(GameObject collectableGameObject)
        {
            collectableGameObject.transform.parent = transform;
            collectableGameObject.tag = "Collected";
            Vector3 newPos;

            newPos = new Vector3(0, (_stackData.OffsetY * 0.1f) * (CollectableStack.Count), 0);



            //collectableGameObject.transform.DOLocalMove(newPos, 1f).OnComplete(() => SetPosition(collectableGameObject, newPos)).SetEase(Ease.InOutBack);
            collectableGameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            CollectableStack.Add(collectableGameObject);
            SetPosition(collectableGameObject, newPos);

        }

        private void SetPosition(GameObject _collectableGameObject, Vector3 newPos)
        {
            _collectableGameObject.transform.localPosition = newPos;

        }

        private void OnReset()
        {
            foreach (Transform childs in transform)
            {
                Destroy(childs.gameObject);
            }
            CollectableStack.Clear();
        }

        public void ReleaseAmmosToTurretArea(GameObject releaseObject)
        {
            StartCoroutine(ReleaseAmmosToTurret(releaseObject));
        }
        private IEnumerator ReleaseAmmosToTurret(GameObject releaseObject)
        {
            foreach (var i in CollectableStack)
            {
                yield return new WaitForSeconds(0.05f);
                i.transform.parent = releaseObject.transform;
                //i.transform.DOMove(releaseObject.transform.position, 0.2f);
                i.transform.position = releaseObject.transform.position;
            }
            CollectableStack.Clear();
        }

        public void GetCapacityData()
        {
            List<int> upgradeList = SaveSignals.Instance.onGetWorkerUpgrades();
            if (upgradeList.Count < 2)
            {
                upgradeList = new List<int>() { 2, 0 };
            }
            Capacity = upgradeList[0] + 1;
        }

        public void OnUpgradeWorkerCapacityData(List<int> upgradeList)
        {
            if (upgradeList.Count < 2)
            {
                upgradeList = new List<int>() { 2, 0 };
            }
            Capacity = upgradeList[0] + 1;
        }


        ////////////////////////////////////////////////
        ///
        public void ReleaseCollectablesToBase()
        {
            if (CollectableStack.Count > 0)
            {
                ScoreSignals.Instance.onScoreIncrease?.Invoke(ScoreTypeEnums.Money, CollectableStack.Count);
                StartCoroutine(Wait05s());
            }

        }

        private IEnumerator Wait05s()
        {
            Temp.Clear();
            foreach (var i in CollectableStack)
            {
                Temp.Add(i);
            }

            CollectableStack.Clear();


            int count = Temp.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                Vector3 pos1 = new Vector3(Temp[i].transform.localPosition.x + Random.Range(-0.8f, 0.8f), Temp[i].transform.localPosition.y + 1, Temp[i].transform.localPosition.z + Random.Range(-0.8f, 0.8f));
                Vector3 pos2 = new Vector3(Temp[i].transform.localPosition.x + Random.Range(-0.8f, 0.8f), Temp[i].transform.localPosition.y - 5, Temp[i].transform.localPosition.z + Random.Range(-0.8f, 0.8f));
                Temp[i].transform.DOLocalPath(new Vector3[2] { pos1, pos2 }, 0.5f);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
            RemoveItem();
        }

        private void RemoveItem()
        {
            foreach (var i in Temp)
            {
                Destroy(i.gameObject);
            }
            Temp.Clear();
        }

    }
}