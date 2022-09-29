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
    public class TurretAmmoAreaController : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables

        #endregion
        #region Serialized Variables
        [SerializeField] private TurretManager manager;

        #endregion

        #region Private Variables
        private int _indeks = 0;
        private List<Vector3> _locations;
        #endregion
        #endregion

        private void Awake()
        {
            _locations = new List<Vector3>() { 
                new Vector3(-0.2f, 0, 0.2f), 
                new Vector3(0.2f, 0, 0.2f), 
                new Vector3(0.2f, 0, -0.2f), 
                new Vector3(-0.2f, 0, -0.2f)
            };
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collected"))
            {
                manager.AmmoBoxList.Add(other.transform);
                int moddedIndeks = _indeks %_locations.Count;
                other.transform.DOLocalMove(new Vector3(_locations[moddedIndeks].x, (int)(_indeks / 4) * 0.5f , _locations[moddedIndeks].z), 1f);
                StartCoroutine(ResetCollectableRotation(other.transform));
                ++_indeks;
                return;
            }
    
        }

        private IEnumerator ResetCollectableRotation(Transform ammoBox)
        {
            yield return new WaitForSeconds(0.5f);
            ammoBox.rotation = Quaternion.Euler(Vector3.zero);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
    
                return;
            }
        }


    }
}