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
        [SerializeField] private int bulletThrowForce = 1500;

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

        private void OnEnable()
        {
            Move();
            StartCoroutine(Destroy(3f));

        }

        private void OnDisable()
        {
            _rig.velocity = Vector3.zero;
        }
        private void Start()
        {
            //Move();
            //Destroy(gameObject, 3f);

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                StartCoroutine(Destroy(0f));
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
            gameObject.SetActive(false);
        }
    }
}