using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using DG.Tweening;
using System.Collections.Generic;

namespace Controllers
{
    public class GemAreaController : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables

        #endregion
        #region Serialized Variables

        #endregion

        #region Private Variables
        private List<Vector3> _locations;
        [SerializeField] private List<Transform> _gemList;
        private int _indeks = 0;
        private Transform _poolObj;

        #endregion
        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {

            LevelSignals.Instance.onBossDefeated += OnBossDefeated;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onBossDefeated -= OnBossDefeated;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void Awake()
        {
            Init();
            
        }

        private void Init()
        {
            _locations = new List<Vector3>() {
                new Vector3(-0.4f, 0.75f, 0.4f),
                new Vector3(-0.4f, 0.75f, 0f),
                new Vector3(-0.4f, 0.75f, -0.4f),
                new Vector3(0f, 0.75f, 0.4f),
                new Vector3(0f, 0.75f, 0f),
                new Vector3(0f, 0.75f, -0.4f),
                new Vector3(0.4f, 0.75f, 0.4f),
                new Vector3(0.4f, 0.75f, 0f),
                new Vector3(0.4f, 0.75f, -0.4f),
            };

            _gemList = new List<Transform>();
            _poolObj = PoolSignals.Instance.onGetPoolManagerObj();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collected"))
            {
                _indeks = _gemList.Count;
                other.tag = "Gem";
                _gemList.Add(other.transform);
                int moddedIndeks = _indeks % _locations.Count;
                other.transform.DOLocalMove(new Vector3(_locations[moddedIndeks].x, _locations[moddedIndeks].y + (int)(_indeks / 9) * 0.5f, _locations[moddedIndeks].z), 1f);
                if (_gemList.Count > 45)
                {
                    LevelSignals.Instance.onMineGemCapacityFull?.Invoke();
                }

                return;
            }

            if (other.CompareTag("Player"))
            {
                if (_gemList.Count >= 45)
                {
                    LevelSignals.Instance.onMineGemCapacityCleared?.Invoke();

                }
                foreach (var i in _gemList)
                {
                    i.DOMove(other.transform.position, 1f).OnComplete(() => DestroyGems(i));
                }
                _gemList.Clear();
            }
        }

        private void DestroyGems(Transform gem)
        {
            gem.gameObject.SetActive(false);
        }

        public int OnGetGems()
        {
            return _gemList.Count;
        }

        private void OnBossDefeated()
        {
            foreach (var i in _gemList)
            {
                i.parent = _poolObj;
                i.gameObject.SetActive(false);
            }

        }


    }
}