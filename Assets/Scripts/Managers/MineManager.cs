using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Data.UnityObject;
using Controllers;

public class MineManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    #endregion

    #region Serialized Variables
    [SerializeField] private GameObject minerPrefab;
    [SerializeField] private TextMeshPro minerCountText;
    [SerializeField] private GemAreaController gemAreaController;

    #endregion

    #region Private Variables
    private List<int> _unlockDatas = new List<int>();
    #endregion

    #endregion
    private List<int> GetData() => Resources.Load<CD_Miner>("Data/Miner-SoliderCounts/CD_Miner").Data.prices;
    private int _minerCount = 0;
    private int _currentLevel;


    public int MinerCount
    {
        get { return _minerCount; }
        set { _minerCount = value; 
            UpdateText(); }
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        GetCurrentLevel();
        _unlockDatas = GetData();
        GetData();
        InitializeMiners();
        UpdateText();
    }


    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        LevelSignals.Instance.onMinerCountIncreased += OnMinerCountIncreased;
        LevelSignals.Instance.onGetMineRemainCapacity += OnGetRemainCapacity;
        PlayerSignals.Instance.onGetGems += gemAreaController.OnGetGems;
    }

    private void UnsubscribeEvents()
    {

        LevelSignals.Instance.onMinerCountIncreased -= OnMinerCountIncreased;
        LevelSignals.Instance.onGetMineRemainCapacity -= OnGetRemainCapacity;
        PlayerSignals.Instance.onGetGems -= gemAreaController.OnGetGems;


    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion


    private void UpdateText()
    {
        minerCountText.text = MinerCount + "/" + _unlockDatas[_currentLevel];
    }
    private void InitializeMiners()
    {
        int count = LevelSignals.Instance.onGetMinerCount();
        for (int i = 0; i < count; i++)
        {
            Instantiate(minerPrefab, new Vector3(0,0, -100f), transform.rotation, transform);
        }
        MinerCount = count;

    }

    private void GetCurrentLevel()
    {
        _currentLevel = LevelSignals.Instance.onGetCurrentModdedLevel();
    }

    private void OnMinerCountIncreased(int amount)
    {
        MinerCount += amount;
        UpdateText();
    }

    private int OnGetRemainCapacity()
    {
        return _unlockDatas[_currentLevel] - _minerCount;
    }
}
