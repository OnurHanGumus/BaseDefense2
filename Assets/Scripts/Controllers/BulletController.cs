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
        #region Private Variables

        private TrailRenderer _trailRenderer;
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
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        private void OnEnable()
        {
            _rig.velocity = Vector3.zero;
            Move();
            _trailRenderer.enabled = true;
            StartCoroutine(Destroy(3f));

        }

        private void OnDisable()
        {
            _trailRenderer.enabled = false;

            transform.localPosition = Vector3.zero;
            _rig.velocity = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
            {
                StartCoroutine(Destroy(0f));
                return;
            }
        }

        private void Move()
        {
            _trailRenderer.Clear();
            _rig.AddRelativeForce(Vector3.forward * bulletThrowForce, ForceMode.Force);
        }

        private IEnumerator Destroy(float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }
    }
}