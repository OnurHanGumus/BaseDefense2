using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;

namespace Controllers
{
    public class EnemyPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private EnemyManager manager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.ChangeState(EnemyState.Run);

                return;
            }

        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.SetDirection((other.transform.position - transform.position).normalized, other.transform);

                return;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.ChangeState(EnemyState.Walk);

                return;
            }
        }
    }
}