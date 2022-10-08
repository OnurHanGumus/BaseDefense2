using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<Transform> activeEnemies;
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
        PlayerSignals.Instance.onEnemyDie += OnEnemyDie;
    }

    private void UnsubscribeEvents()
    {

        LevelSignals.Instance.onBossDefeated -= OnBossDefeated;
        LevelSignals.Instance.onPlayerReachedToNewBase -= OnPlayerReachedToNewBase;
        PlayerSignals.Instance.onEnemyDie -= OnEnemyDie;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion
    private IEnumerator SpawnEnemy()
    {
        if (activeEnemies.Count < 25)
        {
            activeEnemies.Add(Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], new Vector3(Random.Range(-spawnPosX, spawnPosX), transform.position.y, transform.position.z), transform.rotation, transform).transform);
        }
        yield return new WaitForSeconds(2f);
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

    private void OnEnemyDie(Transform diedEnemy)
    {
        if (activeEnemies.Contains(diedEnemy))
        {
            activeEnemies.Remove(diedEnemy);
        }
    }
}
