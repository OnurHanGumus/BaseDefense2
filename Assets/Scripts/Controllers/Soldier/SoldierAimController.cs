using Data.UnityObject;
using Data.ValueObject;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAimController : MonoBehaviour
{
    #region Self Variables
    public List<Transform> TargetList;
    #region Public Variables
    #endregion

    #region Serialized Variables

    [SerializeField] private SoldierManager manager;



    #region Private Variables
    private AllGunsData _data;
    private int _firedBulletIndex = 0;
    private SphereCollider _col;
    #endregion
    #endregion

    #endregion
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _col = GetComponent<SphereCollider>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (TargetList.Contains(other.transform))
            {
                return;
            }
            TargetList.Add(other.transform);
            return;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TargetList.Remove(other.transform);
            return;
        }
    }

    public void OnRemoveFromTargetList(Transform deadEnemy)
    {
        if (TargetList.Contains(deadEnemy))
        {
            TargetList.Remove(deadEnemy);

        }
    }

    public void SearchEnemy()
    {
        StartCoroutine(SearchForEnemy());
    }
    private IEnumerator SearchForEnemy()
    {

        if (TargetList.Count > 0)
        {
            _col.enabled = false;
            yield return new WaitForSeconds(0.05f);
            _col.enabled = true;

            StopAllCoroutines();

        }
        else
        {
            if (_col.radius < 25)
            {
                _col.radius += 0.1f;
            }
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(SearchForEnemy());
        }
    }
}
