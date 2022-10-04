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
        private List<Transform> _gemList;
        private int _indeks = 0;

        #endregion
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
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collected"))
            {
                _indeks = _gemList.Count;

                _gemList.Add(other.transform);
                int moddedIndeks = _indeks % _locations.Count;
                other.transform.DOLocalMove(new Vector3(_locations[moddedIndeks].x, _locations[moddedIndeks].y + (int)(_indeks / 9) * 0.5f, _locations[moddedIndeks].z), 1f);
                //StartCoroutine(ResetCollectableRotation(other.transform));

                return;
            }
        }

        private IEnumerator ResetCollectableRotation(Transform gem)
        {
            yield return new WaitForSeconds(0.5f);
            gem.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}