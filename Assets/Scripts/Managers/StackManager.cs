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
    public class StackManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables
        public List<GameObject> CollectableStack = new List<GameObject>();
        public ItemAddOnStackCommand ItemAddOnStack;

        #endregion

        #region Seralized Veriables
        [SerializeField] private GameObject levelHolder;
        #endregion

        #region Private Variables

        private StackData _stackData;
        //private StackMoveController _stackMoveController;
        private ItemRemoveOnStackCommand _itemRemoveOnStackCommand;
        private Transform _playerTransform;
        private Vector3 _rotation;
        private bool _canReleaseCollectablesToBase = true;


        #endregion
        #endregion
        
        private void Awake()
        {
            _stackData = GetStackData();
            Init();
        }

        private void Start()
        {
            //_playerTransform = PlayerSignals.Instance.onGetPlayer();
            _playerTransform = GameObject.FindGameObjectWithTag("PlayerStackPos").transform;
            
        }

        private StackData GetStackData() => Resources.Load<CD_Stack>("Data/CD_Stack").Data;

        private void Init()
        {
            ItemAddOnStack = new ItemAddOnStackCommand(ref CollectableStack, transform, _stackData);
            _itemRemoveOnStackCommand = new ItemRemoveOnStackCommand(ref CollectableStack, ref levelHolder);
        }

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            PlayerSignals.Instance.onInteractionCollectable += OnInteractionWithCollectable;
            PlayerSignals.Instance.onPlayerReachBase += OnReleaseCollectablesToBase;

        }
        private void UnSubscribeEvent()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            PlayerSignals.Instance.onInteractionCollectable -= OnInteractionWithCollectable;
            PlayerSignals.Instance.onPlayerReachBase -= OnReleaseCollectablesToBase;


        }
        private void OnDisable()
        {
            UnSubscribeEvent();
        }
        #endregion

        private void Update()
        {
            StackMove();
        }
        private void StackMove()
        {
            StackFollowPlayer();

            if (gameObject.transform.childCount > 0)
            {
                //_stackMoveController.StackItemsMoveOrigin(_playerTransform.transform.position, CollectableStack);
            }
        }

        private void StackFollowPlayer()
        {
            transform.position = _playerTransform.position;

            transform.rotation = _playerTransform.rotation;

        }

        private void OnInteractionWithCollectable(GameObject collectableGameObject)
        {
            ItemAddOnStack.Execute(collectableGameObject);
            collectableGameObject.tag = "Collected";
        }



        private void OnReset()
        {
            foreach (Transform childs in transform)
            {
                Destroy(childs.gameObject);
            }
            CollectableStack.Clear();
        }

        private void OnReleaseCollectablesToBase()
        {
            if (!_canReleaseCollectablesToBase)
            {
                return;
            }
            if (CollectableStack.Count > 0)
            {
                _canReleaseCollectablesToBase = false;
                StartCoroutine(Wait05s());
            }
            ScoreSignals.Instance.onScoreIncrease?.Invoke(ScoreTypeEnums.Money, CollectableStack.Count);

        }

        private IEnumerator Wait05s()
        {
            int count = CollectableStack.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                Vector3 pos1 = new Vector3(CollectableStack[i].transform.localPosition.x + Random.Range(-4, 4), CollectableStack[i].transform.localPosition.y + 10, CollectableStack[i].transform.localPosition.z + Random.Range(-4, 4));
                Vector3 pos2 = new Vector3(CollectableStack[i].transform.localPosition.x + Random.Range(-4, 4), CollectableStack[i].transform.localPosition.y - 30, CollectableStack[i].transform.localPosition.z + Random.Range(-4, 4));
                CollectableStack[i].transform.DOLocalPath(new Vector3[2] { pos1, pos2 }, 0.5f);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
            RemoveItem();
        }

        private void RemoveItem()
        {
            //Destroy(collectable.gameObject);
            //CollectableStack.Remove(collectable);

            foreach (var i in CollectableStack)
            {
                Destroy(i.gameObject);
            }
            CollectableStack.Clear();
            _canReleaseCollectablesToBase = true;
        }
    }
}