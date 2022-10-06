using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;

public class TentManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables
    [SerializeField] private List<Transform> waitingSoldiers;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private GameObject soldierPrefab;


    #endregion
    #region Private Variables
    private int _readySoldiersEmptySlots = 0;
    #endregion
    #endregion
    void Start()
    {
        StartCoroutine(BecomeSoldier());
        _readySoldiersEmptySlots = LevelSignals.Instance.onGetEmptyReadySoldiersCount();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RescuePerson"))
        {
            if (waitingSoldiers.Contains(other.transform.parent))
            {
                return;
            }
            waitingSoldiers.Add(other.transform.parent);

            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RescuePerson"))
        {
            waitingSoldiers.Remove(other.transform.parent);
            Instantiate(soldierPrefab, other.transform.position, other.transform.rotation);
            Destroy(other.transform.parent.gameObject);
            return;
        }
    }

    private IEnumerator BecomeSoldier()
    {

        if (waitingSoldiers.Count > 0 && _readySoldiersEmptySlots > 0)
        {
            SoldierSignals.Instance.onBecomeSoldier?.Invoke(waitingSoldiers[0], exitPoint);
            waitingSoldiers.RemoveAt(0);
            yield return new WaitForSeconds(1f);
            _readySoldiersEmptySlots = LevelSignals.Instance.onGetEmptyReadySoldiersCount();
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(BecomeSoldier());
    }

    
}
