using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthBarManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables
    public TextMeshPro HealthText;

    #endregion

    #region Serialized Variables
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform healthBar;
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

    private void Update()
    {
        transform.localEulerAngles = new Vector3(30, -playerTransform.eulerAngles.y, 0);
    }

    public void SetHealthBarScale(int currentValue, int maxValue)
    {
        healthBar.localScale = new Vector3((float)currentValue / maxValue, 1, 1);
    }
}
