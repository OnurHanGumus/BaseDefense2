using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    #endregion

    #region Serialized Variables
    [SerializeField] private GameObject minerPrefab;

    #endregion

    #region Private Variables

    #endregion

    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        InitializeMiners();
    }


    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    { 

    }

    private void UnsubscribeEvents()
    {



    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void InitializeMiners()
    {
        int count = LevelSignals.Instance.onGetMinerCount();
        for (int i = 0; i < count; i++)
        {
            Instantiate(minerPrefab, new Vector3(0,0, -100f), transform.rotation);
        }
    }
}
