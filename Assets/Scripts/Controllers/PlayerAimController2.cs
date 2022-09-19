using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class PlayerAimController2 : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager2 manager;
        [SerializeField] private List<Transform> targetList;
        //[SerializeField] private Transform playerRotatablePart;
        [SerializeField] private Transform currentTarget;
        [SerializeField] private Transform targetGameObject;

        [SerializeField] private GameObject currentBullet;
        [SerializeField] private Transform nisangah;




        #endregion

        #endregion


        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            currentBullet = Resources.Load<GameObject>("Bullets/" + manager.CurrentGunId.ToString());
        }

        private void Start()
        {
            StartCoroutine(Shoot());
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (targetList.Contains(other.transform))
                {
                    return;
                }
                targetList.Add(other.transform);
                return;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                targetList.Remove(other.transform);
                return;
            }
        }

        private void Update()
        {
            if (targetList.Count > 0)
            {
                currentTarget = targetList[0];
                targetGameObject.position = currentTarget.position;
            }

            else if (targetList.Count == 0)
            {
                //targetGameObject.localPosition = Vector3.MoveTowards(targetGameObject.transform.localPosition, new Vector3(0, 7.5f, 10f), 1f);
                targetGameObject.localPosition = new Vector3(0, 7.5f, 10f);

            }
        }

        private IEnumerator Shoot()
        {
            if (targetList.Count > 0)
            {
                Instantiate(currentBullet, nisangah.transform.position, nisangah.rotation);
            }
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Shoot());

        }
    }
}