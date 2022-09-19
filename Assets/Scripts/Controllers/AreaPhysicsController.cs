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
        [SerializeField] private SaveLoadStates type;

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
                PlayerSignals.Instance.onPlayerLeaveBuyArea?.Invoke(type);
                StopAllCoroutines();
                return;
            }
        }

        private void HaveEnoughMoney()
        {
            if (ScoreSignals.Instance.onGetMoney() >= manager.UnlockValue)
            {
                StartCoroutine(Buy());
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