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
            Destroy(gameObject, 3f);

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                return;
            }

        }

        private void Move()
        {
            _rig.AddRelativeForce(Vector3.forward * 1500f, ForceMode.Force);
        }
    }
}