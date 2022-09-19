using Enums;
using Keys;
using Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Controllers
{
    public class PlayerRiggingController1 : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager2 manager;
        [SerializeField] private RigBuilder rigBuilder;
        [SerializeField] private List<GameObject> guns;


        #endregion

        #endregion


        public void SetAnimationRig(bool isOnBase, int gunType)
        {
            if (isOnBase.Equals(true))
            {
                guns[gunType].SetActive(false);

                ResetAnimationRig();
                return;
            }

            if (gunType.Equals(0) || gunType.Equals(2))
            {
                rigBuilder.layers[1].active = true;
                rigBuilder.layers[2].active = false;


            }
            else
            {
                rigBuilder.layers[2].active = true;
                rigBuilder.layers[1].active = false;

            }
            SetAllGunsDeactive();
            guns[gunType].SetActive(true);

            SetSpesificRigActiveness(0, true);
        }
        private void ResetAnimationRig()
        {
            for (int i = 0; i < rigBuilder.layers.Count; i++)
            {
                SetSpesificRigActiveness(i, false);
            }
        }

        private void SetSpesificRigActiveness(int index, bool value)
        {
            rigBuilder.layers[index].active = value;

        }

        private void SetAllGunsDeactive()
        {
            foreach (var i in guns)
            {
                i.SetActive(false);
            }
        }
    }
}