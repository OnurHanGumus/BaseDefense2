using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using Data.ValueObject;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Controllers
{
    public class WorkerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private WorkerStackManager stackManager;



        #endregion
        #region Private Variables

        #endregion
        #endregion


        private void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                stackManager.InteractionWithCollectable(other.gameObject);
                return;
            }
            if (other.CompareTag("TurretAmmoArea"))
            {
                stackManager.ReleaseAmmosToTurretArea(other.gameObject);
                return;
            }
        }
    }
}