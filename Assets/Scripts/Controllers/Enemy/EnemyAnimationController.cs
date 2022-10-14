using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using Data.ValueObject;
using Sirenix.OdinInspector;

namespace Controllers
{
    public class EnemyAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables



        #endregion
        #region Private Variables
        private Animator _animator;


        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetAnimation(EnemyAnimationState animationState)
        {
            _animator.SetTrigger(animationState.ToString());

        }
    }
}