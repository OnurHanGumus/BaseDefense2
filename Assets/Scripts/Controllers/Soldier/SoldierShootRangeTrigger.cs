using Data.UnityObject;
using Data.ValueObject;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierShootRangeTrigger : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    //public bool IsEnemyNear = false;
    public List<Transform> TargetList;

    #endregion

    #region Serialized Variables

    [SerializeField] private SoldierManager manager;
    [SerializeField] private GameObject currentBullet;
    [SerializeField] private Transform nisangah;


    #region Private Variables

    #endregion
    #endregion


    public bool IsEnemyNear
    {
        get { return TargetList.Count > 0; }
    }


    #endregion
    private void Awake()
    {
        Init();
    }

    private void Init()
    {

    }

    private void Start()
    {
        currentBullet = GetBullet();
        StartCoroutine(ShootEnemy());
    }

   

    
    private GameObject GetBullet() => Resources.Load<GameObject>("Bullets/TurretBullet");
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

    private IEnumerator ShootEnemy()
    {
        if (IsEnemyNear)
        {
            Instantiate(currentBullet, nisangah.transform.position, nisangah.rotation);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(ShootEnemy());

    }
}
