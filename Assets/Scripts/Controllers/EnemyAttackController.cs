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
            if (other.CompareTag("Player"))
            {
                SetAnimation(EnemyAnimationState.Attack);

                return;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SetAnimation(EnemyAnimationState.Walk);

                return;
            }

        }

        private void SetAnimation(EnemyAnimationState animationState)
        {
            animator.SetTrigger(animationState.ToString());

        }
    }
}