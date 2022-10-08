using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private int spawnPosX = 20;


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
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        LevelSignals.Instance.onBossDefeated += OnBossDefeated;
        LevelSignals.Instance.onPlayerReachedToNewBase += OnPlayerReachedToNewBase;
    }

    private void UnsubscribeEvents()
    {

        LevelSignals.Instance.onBossDefeated -= OnBossDefeated;
        LevelSignals.Instance.onPlayerReachedToNewBase -= OnPlayerReachedToNewBase;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion
    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], new Vector3(Random.Range(-spawnPosX, spawnPosX), transform.position.y, transform.position.z), transform.rotation, transform);
        StartCoroutine(SpawnEnemy());
    }

    private void OnBossDefeated()
    {
        StopAllCoroutines();
    }
    private void OnPlayerReachedToNewBase()
    {
        StartCoroutine(SpawnEnemy());
    }
}
