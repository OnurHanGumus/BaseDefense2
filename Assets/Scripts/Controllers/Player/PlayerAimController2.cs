using Data.UnityObject;
using Data.ValueObject;
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

        #region Private Variables
        private AllGunsData _data;

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
        private GameObject GetBullet() => Resources.Load<GameObject>("Bullets/" + manager.CurrentGunId.ToString());


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
            if (targetList.Count > 0)
            {
                
                currentTarget = targetList[0];
                if (currentTarget.Equals(null))
                {
                    targetList.RemoveAt(0);
                    return;
                }
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
            if (manager.IsPlayerDead || manager.IsOnBase)
            {
                //just wait
            }

            else if (targetList.Count > 0)
            {
                Instantiate(currentBullet, nisangah.transform.position, nisangah.rotation);
            }
            yield return new WaitForSeconds(_data.guns[manager.CurrentGunId].Delay);
            StartCoroutine(Shoot());

        }

        public void OnRemoveFromTargetList(Transform deadEnemy)
        {
            if (targetList.Contains(deadEnemy))
            {
                targetList.Remove(deadEnemy);
            }
        }

        public void SetGunSettings(Transform nisangah)
        {
            this.nisangah = nisangah;
            currentBullet = GetBullet();
            GetData();
        }
    }
}