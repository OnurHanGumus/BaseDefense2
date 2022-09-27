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

        #region Serialized Variables


        #endregion

        #region Private Variables
        private int _indeks = 0;
        private List<Vector3> _locations;
        private int _floor = 0;
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
                int moddedIndeks = _indeks %_locations.Count;
                //Debug.Log((_indeks / (_locations.Count - 1)) * 0.5f);
                other.transform.DOLocalMove(new Vector3(_locations[moddedIndeks].x, 0, _locations[moddedIndeks].z), 0.5f);
                other.transform.rotation = Quaternion.Euler(Vector3.zero);
                ++_indeks;
                
                return;
            }
    
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