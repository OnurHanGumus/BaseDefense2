using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;

namespace Controllers
{
    public class EnemyAttackController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private EnemyManager manager;

        #endregion

        #endregion

      private void OnTriggerEnter(Collider other)
        {
            if (manager.State.Equals(EnemyState.Die))
            {
                return;
            }
            if (other.CompareTag("Player") || other.CompareTag("Soldier"))
            {
                manager.ChangeAnimState(EnemyAnimationState.Attack);
                manager.ChangeState(EnemyState.Deactive);
                return;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (manager.State.Equals(EnemyState.Die))
            {
                return;
            }
            if (other.CompareTag("Player") || other.CompareTag("Soldier"))
            {
                manager.ChangeAnimState(EnemyAnimationState.Walk);
                manager.ChangeState(EnemyState.Run);


                return;
            }

        }

        
    }
}