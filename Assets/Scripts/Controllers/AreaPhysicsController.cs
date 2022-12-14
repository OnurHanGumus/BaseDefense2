using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;

namespace Controllers
{
    public class AreaPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private AreaManager manager;

        [SerializeField] private ScoreTypeEnums scoreType = ScoreTypeEnums.Money;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //PlayerSignals.Instance.onInteractionCollectable?.Invoke(other.gameObject);
                HaveEnoughMoney();
                return;
            }
    
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("playerExit");
                manager.PlayerLeaveArea();
                StopAllCoroutines();
                return;
            }
        }

        private void HaveEnoughMoney()
        {
            if (scoreType.Equals(ScoreTypeEnums.Money))
            {
                if (ScoreSignals.Instance.onGetMoney() >= manager.UnlockValue)
                {
                    StartCoroutine(Buy());
                }
            }
            else
            {
                if (ScoreSignals.Instance.onGetGem() >= manager.UnlockValue)
                {
                    StartCoroutine(Buy());
                }
            }
            
                 
        }
        private IEnumerator Buy()
        {
            yield return new WaitForSeconds(0.01f);
            if (manager.UnlockValue <= 0)
            {
                StopAllCoroutines();
            }
            else
            {
                StartCoroutine(Buy());
                manager.Pay();
            }
        }
    }
}