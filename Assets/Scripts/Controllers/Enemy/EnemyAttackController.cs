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
            if (other.CompareTag("Player") || other.CompareTag("Soldier"))
            {
                SetAnimation(EnemyAnimationState.Attack);

                return;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Soldier"))
            {
                SetAnimation(EnemyAnimationState.Walk);

                return;
            }

        }

        public void SetAnimation(EnemyAnimationState animationState)
        {
            animator.SetTrigger(animationState.ToString());

        }
    }
}