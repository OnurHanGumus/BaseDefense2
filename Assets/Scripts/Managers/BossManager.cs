using Data.UnityObject;
using Data.ValueObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class BossManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables
    public BossStates State = BossStates.Idle;
    #endregion

    #region Serialized Variables
    [SerializeField] private BossAnimationController animationController;
    [SerializeField] private BossAimController aimController;

    #endregion

    #region Private Variables
    private BossRotationController _rotationController;
    #endregion

    #endregion

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _rotationController = GetComponent<BossRotationController>();
    }
    private SoldierData GetData() => Resources.Load<CD_Soldier>("Data/CD_Soldier").Data;

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

    private void FixedUpdate()
    {
        if (State.Equals(BossStates.Idle))
        {
            if (aimController.Target != null)
            {
                State = BossStates.Attack;
                animationController.SetAnimState(BossStates.Attack);
            }
        }
        else if (State.Equals(BossStates.Attack))
        {
            if (aimController.Target == null)
            {
                State = BossStates.Idle;
                animationController.SetAnimState(BossStates.Idle);
                return;
            }
            _rotationController.RotateCharacterToTarget(aimController.Target.position);

        }
    }
}
