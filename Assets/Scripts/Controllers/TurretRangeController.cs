using Data.UnityObject;
using Data.ValueObject;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class TurretRangeController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TurretManager manager;
        [SerializeField] private List<Transform> targetList;
        //[SerializeField] private Transform playerRotatablePart;
        [SerializeField] private Transform currentTarget;
        [SerializeField] private Transform turretRotatableObj;

        [SerializeField] private GameObject currentBullet;
        [SerializeField] private Transform nisangah;

        #region Private Variables
        private AllGunsData _data;
        private int _firedBulletIndex = 0;
        #endregion
        #endregion

        #endregion


        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _data = GetData();
        }
        private AllGunsData GetData() => Resources.Load<CD_Gun>("Data/CD_Gun").Data;
        private GameObject GetBullet() => Resources.Load<GameObject>("Bullets/TurretBullet");


        private void Start()
        {
            StartCoroutine(Shoot());
            currentBullet = GetBullet();
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
            if (manager.HasOwner && targetList.Count > 0 && manager.AmmoBoxList.Count > 0)
            {

                currentTarget = targetList[0];
                if (currentTarget.Equals(null))
                {
                    targetList.RemoveAt(0);
                    return;
                }
                Vector3 lookAtPos = new Vector3(currentTarget.position.x, 0, currentTarget.position.z);
                turretRotatableObj.LookAt(lookAtPos);
            }

            else if (targetList.Count == 0)
            {
                //targetGameObject.localPosition = Vector3.MoveTowards(targetGameObject.transform.localPosition, new Vector3(0, 7.5f, 10f), 1f);
                //turretRotatableObj.localPosition = new Vector3(0, 7.5f, 10f);

            }
        }

        private IEnumerator Shoot()
        {
            if (manager.HasOwner && targetList.Count > 0 && manager.AmmoBoxList.Count > 0)
            {
                UseAmmo();
                Instantiate(currentBullet, nisangah.transform.position, nisangah.rotation);
            }
            yield return new WaitForSeconds(1f);
            StartCoroutine(Shoot());

        }

        private void UseAmmo()
        {
            _firedBulletIndex++;
            if (_firedBulletIndex.Equals(3))
            {
                Destroy(manager.AmmoBoxList[manager.AmmoBoxList.Count - 1].gameObject);
                manager.AmmoBoxList.RemoveAt(manager.AmmoBoxList.Count - 1);
                _firedBulletIndex = 0;
            }

        }
        public void OnRemoveFromTargetList(Transform deadEnemy)
        {
            if (targetList.Contains(deadEnemy))
            {
                targetList.Remove(deadEnemy);
            }
        }

    }
}