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
        [SerializeField] private Animator animator;


        #endregion
        #region Private Variables
        #endregion
        #endregion

        public void SetAnimState(RescuePersonAnimStates animState)
        {
            animator.SetTrigger(animState.ToString());
        }

        public void SetSpeedVariable(float speed)
        {
            animator.SetFloat("Speed", speed);
        }
    }
}