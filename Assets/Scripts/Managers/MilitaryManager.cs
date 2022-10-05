using Data.UnityObject;
using Signals;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MilitaryManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    #endregion

    #region Serialized Variables
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private TextMeshPro soldierCountText;

    #endregion

    #region Private Variables
    private List<int> _unlockDatas = new List<int>();
    #endregion

    #endregion
    private List<int> GetData() => Resources.Load<CD_SoldierArea>("Data/Miner-SoliderCounts/CD_SoldierArea").Data.prices;
    private int _soldierCount = 0;
    private int _currentLevel;


    public int SoldierCount
    {
        get { return _soldierCount; }
        set
        {
            _soldierCount = value;
            UpdateText();
        }
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
        //InitializeMiners();
        UpdateText();
    }


    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        LevelSignals.Instance.onSoldierCountIncreased += OnSoldierCountIncreased;
        LevelSignals.Instance.onGetMilitaryTotalCapacity += OnGetMilitaryAreaRemainCapacity;
    }

    private void UnsubscribeEvents()
    {

        LevelSignals.Instance.onSoldierCountIncreased -= OnSoldierCountIncreased;
        LevelSignals.Instance.onGetMilitaryTotalCapacity -= OnGetMilitaryAreaRemainCapacity;


    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion


    private void UpdateText()
    {
        soldierCountText.text = SoldierCount + "/" + _unlockDatas[_currentLevel];
    }
    //private void InitializeMiners()
    //{
    //    int count = LevelSignals.Instance.onGetMinerCount();
    //    for (int i = 0; i < count; i++)
    //    {
    //        Instantiate(soldierPrefab, new Vector3(0, 0, -100f), transform.rotation);
    //    }
    //    SoldierCount = count;

    //}

    private void GetCurrentLevel()
    {
        _currentLevel = LevelSignals.Instance.onGetCurrentModdedLevel();
    }

    private void OnSoldierCountIncreased(int amount)
    {
        SoldierCount += amount;
        UpdateText();
    }

    private int OnGetMilitaryAreaRemainCapacity()
    {
        return _unlockDatas[_currentLevel] - SoldierCount;
    }
}
