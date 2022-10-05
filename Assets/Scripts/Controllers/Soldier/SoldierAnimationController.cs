using Enums;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAnimationController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private SoldierManager manager;
    [SerializeField] private Animator animator;


    #endregion
    #region Private Variables
    #endregion
    #endregion

    public void SetAnimState(SoldierAnimStates animState)
    {
        animator.SetTrigger(animState.ToString());
    }

    public void SetSpeedVariable(float speed)
    {
        animator.SetFloat("Speed", speed);
    }
}
