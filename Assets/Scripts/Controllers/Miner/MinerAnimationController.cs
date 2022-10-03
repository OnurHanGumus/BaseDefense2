using Enums;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class MinerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;


        #endregion
        #region Private Variables
        #endregion
        #endregion

        public void SetAnimState(MinerAnimStates animState)
        {
            animator.SetTrigger(animState.ToString());
        }

    }
}