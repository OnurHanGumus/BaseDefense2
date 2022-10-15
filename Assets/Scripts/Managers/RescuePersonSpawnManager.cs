using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuePersonSpawnManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private GameObject rescuePersonPrefab;
    [SerializeField] private List<Transform> activeRescuePersons;
    [SerializeField] private int spawnPosX = 20;


    #endregion
    #region Private Variables
    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
    }
    #region Event Subscriptions
    private void Start()
    {
        StartCoroutine(SpawnRescuePerson());
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
    private IEnumerator SpawnRescuePerson()
    {
        for (int i = 0; i < activeRescuePersons.Count; i++)
        {
            if (activeRescuePersons[i] == null)
            {
                activeRescuePersons.RemoveAt(i);
            }
        }

        if (activeRescuePersons.Count < 5)
        {
            GameObject person = Instantiate(rescuePersonPrefab, transform);

            person.gameObject.SetActive(true);
            person.transform.position = new Vector3(Random.Range(-spawnPosX, spawnPosX), person.transform.position.y, Random.Range(50, 280));
            activeRescuePersons.Add(person.transform);
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnRescuePerson());

    }
    private void OnBossDefeated()
    {
        StopAllCoroutines();
    }
    private void OnPlayerReachedToNewBase()
    {
        StartCoroutine(SpawnRescuePerson());
    }

    private void OnEnemyDie(Transform diedEnemy)
    {
        if (activeRescuePersons.Contains(diedEnemy))
        {
            activeRescuePersons.Remove(diedEnemy);
        }
    }
}
