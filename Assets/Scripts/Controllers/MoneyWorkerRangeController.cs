using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using System.Collections.Generic;

namespace Controllers
{
    public class MoneyWorkerRangeController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public List<Transform> MoneyList;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                if (MoneyList.Contains(other.transform))
                {
                    return;
                }
                MoneyList.Add(other.transform);
                return;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                return;
            }

        }

        public void OnMoneyOnListCollected(Transform collectedMoney)
        {
            if (MoneyList.Contains(collectedMoney))
            {
                MoneyList.Remove(collectedMoney);
            }
        }
    }
}