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
        public List<GameObject> Temp = new List<GameObject>();

        public ItemAddOnStackCommand ItemAddOnStack;

        #endregion

        #region Seralized Veriables
        [SerializeField] private GameObject levelHolder;
        #endregion

        #region Private Variables

        private StackData _stackData;
        private Transform _playerTransform;
        private bool _canReleaseCollectablesToBase = true;
        private bool _isReleasingAmmos = false;
        private Transform _poolObj;


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
            _poolObj = PoolSignals.Instance.onGetPoolManagerObj();
        }

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            PlayerSignals.Instance.onInteractionCollectable += OnInteractionWithCollectable;
            PlayerSignals.Instance.onPlayerReachBase += OnReleaseCollectablesToBase;
            PlayerSignals.Instance.onPlayerReachTurretAmmoArea += OnReleaseAmmosToTurretArea;
            SaveSignals.Instance.onInitializePlayerUpgrades += ItemAddOnStack.OnGetCarryLevel;
            SaveSignals.Instance.onUpgradePlayer += ItemAddOnStack.OnGetCarryLevel;
            PlayerSignals.Instance.onPlayerDie += OnPlayerDie;

            StackSignals.Instance.onGetStackRemainPlace += OnGetStackCount;
            
            

        }
        private void UnSubscribeEvent()
        {
            PlayerSignals.Instance.onInteractionCollectable -= OnInteractionWithCollectable;
            PlayerSignals.Instance.onPlayerReachBase -= OnReleaseCollectablesToBase;
            PlayerSignals.Instance.onPlayerReachTurretAmmoArea -= OnReleaseAmmosToTurretArea;
            SaveSignals.Instance.onInitializePlayerUpgrades -= ItemAddOnStack.OnGetCarryLevel;
            SaveSignals.Instance.onUpgradePlayer -= ItemAddOnStack.OnGetCarryLevel;
            PlayerSignals.Instance.onPlayerDie -= OnPlayerDie;

            StackSignals.Instance.onGetStackRemainPlace -= OnGetStackCount;
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
        }

        private void StackFollowPlayer()
        {
            transform.position = _playerTransform.position;
            transform.rotation = _playerTransform.rotation;

        }

        private void OnInteractionWithCollectable(GameObject collectableGameObject)
        {
            ItemAddOnStack.Execute(collectableGameObject);
        }

        private void OnPlayerCloseWinPanel()
        {
            foreach (Transform childs in transform)
            {
                //Destroy(childs.gameObject);
                childs.transform.parent = null;
                childs.gameObject.SetActive(false);
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
                ScoreSignals.Instance.onScoreIncrease?.Invoke(ScoreTypeEnums.Money, CollectableStack.Count);
                ItemAddOnStack.ResetTowerCount();
                _canReleaseCollectablesToBase = false;
                StartCoroutine(Wait05s());
            }
            StackSignals.Instance.onStackIncreased?.Invoke(0);


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
                Vector3 pos1 = new Vector3(Temp[i].transform.localPosition.x + Random.Range(-4, 4), Temp[i].transform.localPosition.y + 10, Temp[i].transform.localPosition.z + Random.Range(-4, 4));
                Vector3 pos2 = new Vector3(Temp[i].transform.localPosition.x + Random.Range(-4, 4), Temp[i].transform.localPosition.y - 30, Temp[i].transform.localPosition.z + Random.Range(-4, 4));
                Temp[i].transform.DOLocalPath(new Vector3[2] { pos1, pos2 }, 0.3f);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.3f);
            RemoveItem();
        }

        private void RemoveItem()
        {
            foreach (var i in Temp)
            {
                //Destroy(i.gameObject);
                i.transform.parent = _poolObj;
                i.gameObject.SetActive(false);
            }
            Temp.Clear();
            _canReleaseCollectablesToBase = true;
        }

        private void OnReleaseAmmosToTurretArea(GameObject releaseObject)
        {
            if (_isReleasingAmmos)
            {
                return;
            }
            _isReleasingAmmos = true;
            ItemAddOnStack.ResetTowerCount();
            StartCoroutine(ReleaseAmmosToTurret(releaseObject));
        }
        private IEnumerator ReleaseAmmosToTurret(GameObject releaseObject)
        {
            foreach (var i in CollectableStack)
            {
                i.tag = "CollectedAmmo";
                yield return new WaitForSeconds(0.05f);
                i.transform.parent = releaseObject.transform;
                //i.transform.DOMove(releaseObject.transform.position, 0.2f);
                i.transform.position = releaseObject.transform.position;
            }
            CollectableStack.Clear();
            _isReleasingAmmos = false;

        }

        private void OnPlayerDie()
        {
            ItemAddOnStack.ResetTowerCount();

            foreach (var i in CollectableStack)
            {
                //Destroy(i.gameObject);
                i.transform.parent = _poolObj;
                i.gameObject.SetActive(false);
            }
            foreach (var i in Temp)
            {
                //Destroy(i.gameObject);
                i.transform.parent = null;
                i.gameObject.SetActive(false);
            }
            CollectableStack.Clear();
            Temp.Clear();
            _canReleaseCollectablesToBase = true;

        }

        private int OnGetStackCount()
        {
            return ((ItemAddOnStack.CarryLevel * 10) - CollectableStack.Count);
        }
    }
}