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
        
        #region Public Variables
        public List<GameObject> CollectableStack = new List<GameObject>();
        public List<GameObject> Temp = new List<GameObject>();

        public ItemAddOnStackCommand ItemAddOnStack;

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
            ItemAddOnStack = new ItemAddOnStackCommand(ref CollectableStack, transform, _stackData);
          
        }

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            SaveSignals.Instance.onInitializePlayerUpgrades += ItemAddOnStack.OnGetCarryLevel;
            SaveSignals.Instance.onUpgradePlayer += ItemAddOnStack.OnGetCarryLevel;

            

        }
        private void UnSubscribeEvent()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            SaveSignals.Instance.onInitializePlayerUpgrades -= ItemAddOnStack.OnGetCarryLevel;
            SaveSignals.Instance.onUpgradePlayer -= ItemAddOnStack.OnGetCarryLevel;

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
            ItemAddOnStack.ResetTowerCount();
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
    }
}