using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using System.Collections.Generic;

namespace Controllers
{
    public class ShotgunBulletController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private int bulletThrowForce = 1500;
        [SerializeField] private int rotation;
        #endregion
        #region Private Variables
        private Rigidbody _rig;
        #endregion
        #endregion
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rig = GetComponent<Rigidbody>();

        }

        private void OnEnable()
        {
            Move();
            StartCoroutine(Destroy(3f));

        }

        

        private void OnDisable()
        {
            _rig.velocity = Vector3.zero;
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = new Vector3(0, rotation, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                //gameObject.SetActive(false);
                return;
            }
        }

        private void Move()
        {
            _rig.AddRelativeForce(Vector3.forward * bulletThrowForce, ForceMode.Force);
        }

        private IEnumerator Destroy(float delay)
        {
            yield return new WaitForSeconds(delay);
            transform.parent.gameObject.SetActive(false);
        }
    }
}