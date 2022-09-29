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
    public class TurretPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TurretManager manager;



        #endregion
        #region Private Variables

        #endregion
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !manager.HasOwner)
            {
                PlayerSignals.Instance.onPlayerUseTurret?.Invoke(true);
                manager.PlayerUseTurret(other.transform);
                return;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && !manager.HasOwner)
            {
                manager.PlayerLeaveTurret(other.transform);
                return;
            }
        }




    }
}