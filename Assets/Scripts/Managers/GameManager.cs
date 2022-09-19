using System;
using Enums;
using Extentions;
using Keys;
using Signals;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public GameStates States;

    #endregion

    #endregion

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }


    private void OnEnable()
    {
        SubscribeEvents();
    }


    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void OnChangeGameState(GameStates newState)
    {
        States = newState;
    }


}