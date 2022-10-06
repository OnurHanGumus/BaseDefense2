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
    public bool IsEnemyNear = false;
    public Transform ShootTarget;

    #endregion

    #region Serialized Variables

    [SerializeField] private SoldierManager manager;
    [SerializeField] private GameObject currentBullet;
    [SerializeField] private Transform nisangah;


    #region Private Variables
    private AllGunsData _data;
    private int _firedBulletIndex = 0;
    private bool _isShooting = false;

    #endregion
    #endregion

    #endregion
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _data = GetData();
    }

    private void Start()
    {
        currentBullet = GetBullet();
        StartCoroutine(ShootEnemy());
    }

   

    
    private AllGunsData GetData() => Resources.Load<CD_Gun>("Data/CD_Gun").Data;
    private GameObject GetBullet() => Resources.Load<GameObject>("Bullets/TurretBullet");
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ShootTarget = other.transform;
            IsEnemyNear = true;
            return;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ShootTarget = null;
            IsEnemyNear = false;

            return;
        }
    }

    public void OnRemoveFromTargetList(Transform deadEnemy)
    {
        if (ShootTarget == deadEnemy)
        {
            ShootTarget = null;
            IsEnemyNear = false;
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
