using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using Enums;

public class PoolManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private GameObject moneyPrefab;
    //[SerializeField] private List<GameObject> gunPrefabs;


    [SerializeField] private List<GameObject> enemyPool;
    [SerializeField] private List<GameObject> gemPool;
    [SerializeField] private List<GameObject> moneyPool;
    [SerializeField] private List<GameObject> bulletPool;

    //[SerializeField] private List<GameObject> pistolBulletPool;
    //[SerializeField] private List<GameObject> shotgunBulletPool;
    //[SerializeField] private List<GameObject> smgBulletPool;
    //[SerializeField] private List<GameObject> assaultRiffleBulletPool;
    //[SerializeField] private List<GameObject> rocketLauncherBulletPool;
    //[SerializeField] private List<GameObject> minigunBulletPool;



    [SerializeField] private int amountEnemyToPool = 25;
    [SerializeField] private int amountGemToPool = 70;
    [SerializeField] private int amountMoneyToPool = 100;
    //[SerializeField] private int amountPistolBulletToPool = 20, amountSMGBulletToPool = 25, amountShotgunBulletToPool = 20, amountAssaultRiffleBulletToPool = 30, amountRocketBulletToPool = 15, amountMinigunBulletToPool = 60;


    #endregion
    #region Private Variables
    private int _levelId = 0;
    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _levelId = LevelSignals.Instance.onGetCurrentModdedLevel();
        InitializeEnemyPool();
        InitializeGemPool();
        InitializeMoneyPool();
        //InitializeBulletPool();
    }

    
    #region Event Subscriptions
    void Start()
    {

    }
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PoolSignals.Instance.onGetEnemyFromPool += OnGetEnemy;
        PoolSignals.Instance.onGetGemFromPool += OnGetGem;
        PoolSignals.Instance.onGetMoneyFromPool += OnGetMoney;
        PoolSignals.Instance.onGetBulletFromPool += OnGetBullet;


        PoolSignals.Instance.onGetPoolManagerObj += OnGetPoolManagerObj;

        PoolSignals.Instance.onAddBulletToPool += OnAddBulletToPool;

        PlayerSignals.Instance.onPlayerSelectGun += OnPlayerSelectGun;

    }

    private void UnsubscribeEvents()
    {
        PoolSignals.Instance.onGetEnemyFromPool -= OnGetEnemy;
        PoolSignals.Instance.onGetGemFromPool -= OnGetGem;
        PoolSignals.Instance.onGetMoneyFromPool -= OnGetMoney;
        PoolSignals.Instance.onGetBulletFromPool -= OnGetBullet;

        PoolSignals.Instance.onGetPoolManagerObj -= OnGetPoolManagerObj;

        PoolSignals.Instance.onAddBulletToPool -= OnAddBulletToPool;

        PlayerSignals.Instance.onPlayerSelectGun -= OnPlayerSelectGun;


    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion


    private void InitializeEnemyPool()
    {
        enemyPool = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountEnemyToPool; i++)
        {
            tmp = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], transform);
            tmp.SetActive(false);
            enemyPool.Add(tmp);
        }
    }

    private void InitializeGemPool()
    {
        gemPool = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountGemToPool; i++)
        {
            tmp = Instantiate(gemPrefab, transform);
            tmp.SetActive(false);
            gemPool.Add(tmp);
        }
    }
    private void InitializeMoneyPool()
    {
        moneyPool = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountMoneyToPool; i++)
        {
            tmp = Instantiate(moneyPrefab, transform);
            tmp.SetActive(false);
            moneyPool.Add(tmp);
        }
    }

    //private void InitializeBulletPool()
    //{
    //    pistolBulletPool = new List<GameObject>();
    //    shotgunBulletPool = new List<GameObject>();
    //    smgBulletPool = new List<GameObject>();
    //    assaultRiffleBulletPool = new List<GameObject>();
    //    rocketLauncherBulletPool = new List<GameObject>();
    //    minigunBulletPool = new List<GameObject>();

    //    GameObject tmp;
    //    for (int i = 0; i < amountPistolBulletToPool; i++)
    //    {
    //        tmp = Instantiate(gunPrefabs[0], transform);
    //        tmp.SetActive(false);
    //        pistolBulletPool.Add(tmp);
    //    }
    //    for (int i = 0; i < amountShotgunBulletToPool; i++)
    //    {
    //        tmp = Instantiate(gunPrefabs[1], transform);
    //        tmp.SetActive(false);
    //        shotgunBulletPool.Add(tmp);
    //    }
    //    for (int i = 0; i < amountSMGBulletToPool; i++)
    //    {
    //        tmp = Instantiate(gunPrefabs[2], transform);
    //        tmp.SetActive(false);
    //        smgBulletPool.Add(tmp);
    //    }
    //    for (int i = 0; i < amountAssaultRiffleBulletToPool; i++)
    //    {
    //        tmp = Instantiate(gunPrefabs[3], transform);
    //        tmp.SetActive(false);
    //        assaultRiffleBulletPool.Add(tmp);
    //    }
    //    for (int i = 0; i < amountRocketBulletToPool; i++)
    //    {
    //        tmp = Instantiate(gunPrefabs[4], transform);
    //        tmp.SetActive(false);
    //        rocketLauncherBulletPool.Add(tmp);
    //    }
    //    for (int i = 0; i < amountMinigunBulletToPool; i++)
    //    {
    //        tmp = Instantiate(gunPrefabs[5], transform);
    //        tmp.SetActive(false);
    //        minigunBulletPool.Add(tmp);
    //    }
    //}
    public GameObject OnGetEnemy()
    {
        for (int i = 0; i < amountEnemyToPool; i++)
        {
            if (!enemyPool[i].activeInHierarchy)
            {
                return enemyPool[i];
            }
        }
        return null;
    }

    public GameObject OnGetGem()
    {
        for (int i = 0; i < amountGemToPool; i++)
        {
            if (!gemPool[i].activeInHierarchy)
            {
                return gemPool[i];
            }
        }
        return null;
    }

    public GameObject OnGetMoney()
    {
        for (int i = 0; i < amountMoneyToPool; i++)
        {
            if (!moneyPool[i].activeInHierarchy)
            {
                return moneyPool[i];
            }
        }
        return null;
    }

    public GameObject OnGetBullet()
    {
        if (bulletPool.Count == 0)
        {
            return null;
        }

        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }
        return null;
    }

    public void OnAddBulletToPool(GameObject bullet)
    {
        bullet.transform.parent = transform;
        bulletPool.Add(bullet);
    }

    public Transform OnGetPoolManagerObj()
    {
        return transform;
    }

    private void OnPlayerSelectGun(int tmep)
    {
        bulletPool.Clear();
    }


}
