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
        private Transform _followObjectTransform;
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _followObjectTransform = transform;

        }

        private void OnTriggerExit(Collider other)
        {

            if (other.CompareTag("Player") && !manager.IsTaken)
            {
                _followObjectTransform = PlayerSignals.Instance.onGetLastRescuePerson();
                manager.SetDirection(_followObjectTransform);
                manager.ChangeState(RescuePersonState.Run);
                manager.ChangeAnim(RescuePersonAnimStates.Taken);

                manager.IsTaken = true;
                PlayerSignals.Instance.onRescuePersonAddedToStack?.Invoke(manager.transform);

                return;
            }


        }
    }
}