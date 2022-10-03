using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class MinerPhysicsController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables
    [SerializeField] private MinerManager manager;



    #endregion
    #region Private Variables

    #endregion
    #endregion
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mine"))
        {
            if (other.transform.Equals(manager.SelectedMine))
            {
                manager.Work(MinerAnimStates.Dig);

            }
            return;
        }

        if (other.CompareTag("Cave"))
        {
            if (other.transform.parent.Equals(manager.SelectedMine))
            {
                manager.Work(MinerAnimStates.Pick);

            }
        }
    }
}
