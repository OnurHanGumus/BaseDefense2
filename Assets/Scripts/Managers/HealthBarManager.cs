using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Managers;
using Signals;

public class HealthBarManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables
    public TextMeshPro HealthText;

    #endregion

    #region Serialized Variables
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform healthBar;
    [SerializeField] private StackManager stackManager;

    [SerializeField] private int stackCurrentNumber = 0;
    #endregion

    #region Private Variables






    #endregion

    #endregion

    #region Event Subscription
    private void OnEnable()
    {
        SubscribeEvent();
    }

    private void SubscribeEvent()
    {
        StackSignals.Instance.onStackIncreased += OnStackIncreased;
        StackSignals.Instance.onStackDecreased += OnStackDecreased;
    }
    private void UnSubscribeEvent()
    {
        StackSignals.Instance.onStackIncreased -= OnStackIncreased;
        StackSignals.Instance.onStackDecreased -= OnStackDecreased;

    }
    private void OnDisable()
    {
        UnSubscribeEvent();
    }
    #endregion


    private void Awake()
    {
        Init();
    }
    private void Init()
    {

    }

    private void Update()
    {
        transform.localEulerAngles = new Vector3(30, -playerTransform.eulerAngles.y, 0);
        transform.position = new Vector3(transform.position.x, 12 + stackCurrentNumber, transform.position.z);
    }

    public void SetHealthBarScale(int currentValue, int maxValue)
    {
        healthBar.localScale = new Vector3((float)currentValue / maxValue, 1, 1);
    }

    private void OnStackIncreased(int value)
    {
        if (value > 10)
        {
            stackCurrentNumber = 10;

        }
        else
        {
            stackCurrentNumber = value;
        }
    }
    private void OnStackDecreased(int value)
    {
        
        stackCurrentNumber = value;

    }
}
