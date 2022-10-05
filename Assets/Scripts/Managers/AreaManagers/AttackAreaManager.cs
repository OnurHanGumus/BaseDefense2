using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<Transform> soldierWaitAreas;
    [SerializeField] private int indeks = 0;


    #endregion
    #region Private Variables

    #endregion
    #endregion

    #region Event Subscriptions
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        SoldierSignals.Instance.onGetSoldierAreaTransform += OnGetSoldierAreaTransform;
    }

    private void UnsubscribeEvents()
    {
        SoldierSignals.Instance.onGetSoldierAreaTransform -= OnGetSoldierAreaTransform;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private Transform OnGetSoldierAreaTransform()
    {
        return soldierWaitAreas[indeks++];
    }
}
