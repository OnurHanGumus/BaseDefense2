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
    public class PlayerPhysicsController1 : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager2 manager;



        #endregion
        #region Private Variables
        private PlayerData _data;
        private int _health = 100;
        #endregion
        #endregion

        private void Awake()
        {

        }

        private void Start()
        {
            _data = manager.GetPlayerData();
            _health = _data.Health;
        }

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

        public void OnGetHealthData(int healthLevel)
        {
            healthLevel += 1;
            _health += (10 * healthLevel);
        }
    }
}