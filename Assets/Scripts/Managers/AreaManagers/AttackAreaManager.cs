using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<Transform> readySoldierAreas;
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
        LevelSignals.Instance.onGetEmptyReadySoldiersCount += OnGetEmptyReadySoldiersCount;
        SoldierSignals.Instance.onBecomeSoldier += OnBecomeSoldier;
    }

    private void UnsubscribeEvents()
    {
        SoldierSignals.Instance.onGetSoldierAreaTransform -= OnGetSoldierAreaTransform;
        LevelSignals.Instance.onGetEmptyReadySoldiersCount -= OnGetEmptyReadySoldiersCount;
        SoldierSignals.Instance.onBecomeSoldier -= OnBecomeSoldier;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private Transform OnGetSoldierAreaTransform()
    {
        return readySoldierAreas[indeks++];
    }

    private int OnGetEmptyReadySoldiersCount()
    {
        Debug.Log(readySoldierAreas.Count - indeks);
        return readySoldierAreas.Count - indeks;
    }

    private void OnBecomeSoldier(Transform transform, Transform transform1)
    {
        LevelSignals.Instance.onSoldierCountIncreased?.Invoke(indeks + 1);
    }

}
