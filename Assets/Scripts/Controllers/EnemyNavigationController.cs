using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using Data.ValueObject;

namespace Controllers
{
    public class EnemyNavigationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private EnemyManager manager;

        #endregion
        
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerOutOfBase"))
            {
                manager.ChangeState(EnemyState.Run);

                return;
            }

        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("PlayerOutOfBase"))
            {
                manager.SetDirection((other.transform.position - transform.position).normalized, other.transform);

                return;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PlayerOutOfBase"))
            {
                manager.ChangeState(EnemyState.Walk);

                return;
            }
        }
    }
}