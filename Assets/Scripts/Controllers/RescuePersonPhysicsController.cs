using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using Data.ValueObject;

namespace Controllers
{
    public class RescuePersonPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private RescuePersonManager manager;

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
                Debug.Log("detected");
                manager.ChangeState(RescuePersonState.Run);
                manager.ChangeAnim(RescuePersonAnimStates.Taken);

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
    }
}