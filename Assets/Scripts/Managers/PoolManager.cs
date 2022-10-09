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
    [SerializeField] private List<GameObject> enemyPool;
    [SerializeField] private int amountToPool = 25;


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
    }
    #region Event Subscriptions
    void Start()
    {
        enemyPool = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], transform);
            tmp.SetActive(false);
            enemyPool.Add(tmp);
        }
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PoolSignals.Instance.onGetEnemyFromPool += OnGetEnemy;
    }

    private void UnsubscribeEvents()
    {
        PoolSignals.Instance.onGetEnemyFromPool -= OnGetEnemy;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    public GameObject OnGetEnemy()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!enemyPool[i].activeInHierarchy)
            {
                return enemyPool[i];
            }
        }
        return null;
    }
}
