using Enums;
using Managers;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPhysicsController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private SoldierManager manager;


    #endregion
    #region Private Variables
    private int _health = 50;

    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _health = manager.GetSoldierData().Health;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damage"))
        {
            if (manager.IsSoldierDead)
            {
                return;
            }
            _health -= 10;
            Debug.Log(_health);
            if (_health <= 0)
            {
                transform.parent.tag = "Untagged";
                manager.IsSoldierDead = true;
                manager.ChangeAnimState(SoldierAnimStates.Die);
                SoldierSignals.Instance.onSoldierDeath?.Invoke(transform.parent);
                StartCoroutine(SoldierDie());
            }
            return;
        }
    }

    private IEnumerator SoldierDie()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(transform.parent.gameObject);
    }


}
