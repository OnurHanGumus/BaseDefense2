using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButtonController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables


    #endregion
    #region Private Variables

    #endregion
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoldierSignals.Instance.onSoldierAttack?.Invoke();
            LevelSignals.Instance.onSoldierCountIncreased?.Invoke(0);
        }
    }
}
