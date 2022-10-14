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
        [SerializeField] private Animator animator;

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
                SetAnimation(EnemyAnimationState.Attack);
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
                SetAnimation(EnemyAnimationState.Walk);
                manager.ChangeState(EnemyState.Run);


                return;
            }

        }

        public void SetAnimation(EnemyAnimationState animationState)
        {
            animator.SetTrigger(animationState.ToString());

        }
    }
}