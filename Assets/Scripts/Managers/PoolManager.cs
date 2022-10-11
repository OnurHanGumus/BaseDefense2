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

    [SerializeField] private List<GameObject> enemyPool;
    [SerializeField] private List<GameObject> gemPool;
    [SerializeField] private List<GameObject> moneyPool;

    [SerializeField] private int amountEnemyToPool = 25;
    [SerializeField] private int amountGemToPool = 70;
    [SerializeField] private int amountMoneyToPool = 100;


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


        PoolSignals.Instance.onGetPoolManagerObj += OnGetPoolManagerObj;

    }

    private void UnsubscribeEvents()
    {
        PoolSignals.Instance.onGetEnemyFromPool -= OnGetEnemy;
        PoolSignals.Instance.onGetGemFromPool -= OnGetGem;
        PoolSignals.Instance.onGetMoneyFromPool -= OnGetMoney;

        PoolSignals.Instance.onGetPoolManagerObj -= OnGetPoolManagerObj;

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

    public Transform OnGetPoolManagerObj()
    {
        return transform;
    }
}
