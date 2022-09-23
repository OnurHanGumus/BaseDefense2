using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;

namespace Controllers
{
    public class FencePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables



        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wall"))
            {
                Destroy(transform.parent.gameObject);
                return;
            }
    
        }

    }
}