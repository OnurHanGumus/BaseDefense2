using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using System.Collections.Generic;

namespace Controllers
{
    public class WorkerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables
        private Animator _an;
        private WorkerAnimStates _currentAnim = WorkerAnimStates.Walk;
        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _an = GetComponent<Animator>();

        }

        public void SetAnimationTrigger(WorkerAnimStates anim)
        {
            if (_currentAnim.Equals(anim))
            {
                return;
            }
            _currentAnim = anim;
            _an.SetTrigger(_currentAnim.ToString());
        }

    }
}