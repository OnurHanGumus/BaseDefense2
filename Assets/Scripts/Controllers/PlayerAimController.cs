using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class PlayerAimController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private List<Transform> targetList;
        //[SerializeField] private Transform playerRotatablePart;
        [SerializeField] private Transform currentTarget;
        [SerializeField] private Transform targetGameObject;

       


        #endregion

        #endregion
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
    }
}