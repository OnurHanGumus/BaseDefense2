using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;

namespace Controllers
{
    public class PlayerPhysicsController1 : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager2 manager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                PlayerSignals.Instance.onInteractionCollectable?.Invoke(other.gameObject);
                return;
            }
            if (other.CompareTag("BaseTrigger"))
            {
                PlayerSignals.Instance.onPlayerReachBase?.Invoke();
                //manager.SetAnimBool(PlayerAnimStates.Base, true);
                manager.SetAnimBool(PlayerAnimStates.Base, true);

                return;
            }

            if (other.CompareTag("OutTrigger"))
            {
                manager.SetAnimBool(PlayerAnimStates.Base, false);

                return;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //if (other.CompareTag("Base"))
            //{
            //    //manager.ResetAnimState(PlayerAnimStates.Base);
            //    manager.SetAnimBool(PlayerAnimStates.Base, false);
            //    return;
            //}
        }
    }
}