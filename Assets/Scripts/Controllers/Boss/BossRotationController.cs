using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
public class BossRotationController : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    #endregion

    #region Serialized Variables


    #endregion

    #region Private Variables
    private Rigidbody _rig;
    #endregion

    #endregion

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _rig = GetComponent<Rigidbody>();

    }
    
    public void RotateCharacterToTarget(Vector3 target)
    {
        transform.LookAt(target);
    }
}
