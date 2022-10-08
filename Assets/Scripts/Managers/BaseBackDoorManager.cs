using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBackDoorManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables


    #endregion

    #region Serialized Variables


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

    }

    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        LevelSignals.Instance.onBossDefeated += OnBossDefeated;
        LevelSignals.Instance.onPlayerReachedToNewBase += OnPlayerReachedNewBase;


    }

    private void UnsubscribeEvents()
    {

        LevelSignals.Instance.onBossDefeated -= OnBossDefeated;
        LevelSignals.Instance.onPlayerReachedToNewBase -= OnPlayerReachedNewBase;

    }

    private void OnBossDefeated()
    {
        gameObject.SetActive(false);
    }
    private void OnPlayerReachedNewBase()
    {
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

}
