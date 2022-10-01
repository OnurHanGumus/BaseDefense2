using Enums;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class RescuePersonAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private RescuePersonManager manager;
        //[SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private Animator animator;


        #endregion
        #region Private Variables
        #endregion
        #endregion

        public void SetAnimState(RescuePersonAnimStates animState)
        {
            animator.SetTrigger(animState.ToString());
        }

        public void SetAnimBool(RescuePersonAnimStates animState, bool value)
        {
            animator.SetBool(animState.ToString(), value);
        }


        public void ResetAnimState(RescuePersonAnimStates animState)
        {
            animator.ResetTrigger(animState.ToString());
        }

        public void SetSpeedVariable(float speed)
        {

            animator.SetFloat("Speed", speed);
        }




    }
}