using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using System.Collections.Generic;

namespace Controllers
{
    public class AmmoAreaPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject bulletBox;
        [SerializeField] private List<GameObject> pool;
        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                int temp = StackSignals.Instance.onGetStackRemainPlace();
                if (temp > pool.Count)
                {
                    StartCoroutine(InstantiateBulletBox(temp - pool.Count));
                }
                return;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(RemoveFromPool());
                return;
            }
        }

        private IEnumerator InstantiateBulletBox(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new WaitForSeconds(0.01f);
                pool.Add(Instantiate(bulletBox, transform.position, transform.rotation));
            }
            
        }

        private IEnumerator RemoveFromPool()
        {
            yield return new WaitForSeconds(2f);

            for (int i = pool.Count - 1; i >= 0; i--)
            {
                if (pool[i].CompareTag("Collected"))
                {
                    pool.RemoveAt(i);

                }
            }
        }
    }
}