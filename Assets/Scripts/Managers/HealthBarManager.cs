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
        transform.localEulerAngles = new Vector3(0, -playerTransform.eulerAngles.y, 0);
    }
}
