using Enums;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager2 manager;
        //[SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private Animator animator;


        #endregion
        #region Private Variables
        private bool _isPlayerUseTurret = false;
        #endregion
        #endregion

        public void SetAnimState(PlayerAnimStates animState)
        {
            animator.SetTrigger(animState.ToString());
        }

        public void SetAnimBool(PlayerAnimStates animState, bool value)
        {
            animator.SetBool(animState.ToString(), value);
        }

  
        public void ResetAnimState(PlayerAnimStates animState)
        {
            animator.ResetTrigger(animState.ToString());
        }

        public void SetSpeedVariable(InputParams inputParams)
        {
            if (_isPlayerUseTurret)
            {
                inputParams.XValue = 0;
                inputParams.ZValue = 0;
            }
            float speedX = Mathf.Abs(inputParams.XValue);
            float speedZ = Mathf.Abs(inputParams.ZValue);
            animator.SetFloat("Run", (speedX + speedZ) / 2);
        }

        public void OnPlayerUseTurret(bool value)
        {
            _isPlayerUseTurret = value;

            if (value.Equals(false))
            {
                SetAnimState(PlayerAnimStates.Base);
            }
        }


    }
}