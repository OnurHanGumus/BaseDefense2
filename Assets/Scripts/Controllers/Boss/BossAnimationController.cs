using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
public class BossAnimationController : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    #endregion

    #region Serialized Variables


    #endregion

    #region Private Variables
    private Animator _animator;

    #endregion

    #endregion

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _animator = GetComponent<Animator>();
    }
    public void SetAnimState(BossStates animState)
    {
        _animator.SetTrigger(animState.ToString());
    }
}
