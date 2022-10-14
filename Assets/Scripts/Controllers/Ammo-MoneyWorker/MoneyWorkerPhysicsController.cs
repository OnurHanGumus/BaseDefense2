using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using Data.ValueObject;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Controllers
{
    public class MoneyWorkerPhysicsController: MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private WorkerStackManager stackManager;
        [SerializeField] private MoneyWorkerRangeController moneyWorkerRangeController;


        #endregion
        #region Private Variables

        #endregion
        #endregion


        private void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                if (moneyWorkerRangeController.MoneyList.Count > 0)
                {
                    if (!other.transform.Equals(moneyWorkerRangeController.MoneyList[0]))
                    {
                        return;
                    }
                }
       
                if (stackManager.CollectableStack.Count < stackManager.Capacity)
                {
                    StackSignals.Instance.onMoneyWorkerCollectMoney?.Invoke(other.transform);
                    stackManager.InteractionWithCollectable(other.gameObject);

                }
                return;
            }
            if (other.CompareTag("BaseTrigger"))
            {
                stackManager.ReleaseCollectablesToBase();
                return;
            }
        }
    }
}