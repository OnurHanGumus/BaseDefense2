using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;

namespace Controllers
{
    public class BulletController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables


        #endregion

        private Rigidbody _rig;
        #endregion
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rig = GetComponent<Rigidbody>();

        }
        private void Start()
        {
            Move();

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //PlayerSignals.Instance.onInteractionCollectable?.Invoke(other.gameObject);
                return;
            }

        }

        private void Move()
        {
            _rig.AddRelativeForce(Vector3.forward, ForceMode.Impulse);
        }
    }
}