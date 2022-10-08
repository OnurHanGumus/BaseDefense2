using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Extentions;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Enums;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public CurrentLevelAreaData Data;

        #endregion

        #region Serialized Variables

        [Space] [SerializeField] private GameObject levelHolder;
        [SerializeField] private LevelLoaderCommand levelLoader;
        [SerializeField] private AreaLoaderCommand areaLoader;
        [SerializeField] private TurretLoaderCommand turretLoader;
        [SerializeField] private MineLoaderCommand mineLoader;
        [SerializeField] private WorkerManagersLoader workerLoader;
        [SerializeField] private ClearActiveLevelCommand levelClearer;
        #endregion

        #region Private Variables

        [ShowInInspector] private int _levelID;
        private int _levelModdedValue;
        private LoadGameCommand _loadGameCommand;
        private SaveGameCommand _saveGameCommand;
        private List<int> _openAreas;
        private List<int> _openTurrets;
        private int _passedBaseCount = 0;


        public int LevelModdedValue
        {
            get { return LevelSignals.Instance.onGetCurrentModdedLevel(); }
            set { _levelModdedValue = value; }
        }



        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _loadGameCommand = new LoadGameCommand();
            _saveGameCommand = new SaveGameCommand();
            _openAreas = new List<int>();
            _openTurrets = new List<int>();

            _levelID = GetActiveLevel();
            Data = GetLevelData();
        }


        private int GetActiveLevel()
        {
            return _loadGameCommand.OnLoadGameData(Enums.SaveLoadStates.Level);
        }

        private List<int> GetActiveAreas()
        {
            return _loadGameCommand.OnLoadList(Enums.SaveLoadStates.CurrentLevelOpenedAreas);
        }

        private List<int> GetActiveTurrets()
        {
            return _loadGameCommand.OnLoadList(Enums.SaveLoadStates.OpenedTurrets, SaveFiles.WorkerCurrentCounts.ToString());
        }
        private List<int> GetActiveTurretOwners()
        {
            return _loadGameCommand.OnLoadList(Enums.SaveLoadStates.OpenedTurretOwners, SaveFiles.WorkerCurrentCounts.ToString());
        }
        private List<int> GetActiveOpenedEnemyAreas()
        {
            return _loadGameCommand.OnLoadList(Enums.SaveLoadStates.OpenedEnemyAreas, SaveFiles.WorkerCurrentCounts.ToString());
        }

        private CurrentLevelAreaData GetLevelData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level").Levels[LevelModdedValue];
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeNextLevel;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
            CoreGameSignals.Instance.onGetLevelID += OnGetLevelID;

            LevelSignals.Instance.onBuyArea += OnBuyArea;
            LevelSignals.Instance.onBuyTurret += OnBuyTurret;
            LevelSignals.Instance.onGetCurrentModdedLevel += OnGetCurrentModdedLevel;
            LevelSignals.Instance.onBossDefeated += OnBossDefeated;

            
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeNextLevel;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
            CoreGameSignals.Instance.onGetLevelID -= OnGetLevelID;

            LevelSignals.Instance.onBuyArea -= OnBuyArea;
            LevelSignals.Instance.onBuyTurret -= OnBuyTurret;
            LevelSignals.Instance.onGetCurrentModdedLevel -= OnGetCurrentModdedLevel;
            LevelSignals.Instance.onBossDefeated -= OnBossDefeated;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            _openAreas = GetActiveAreas();
            _openTurrets = GetActiveTurrets();

            OnInitializeLevel();
            OnInitializeNextLevel();
            InitializeAreas();
            InitializeTurrets();
            MineInitialize();
            WorkerManagerInitialize();
        }

        private void OnNextLevel()
        {
            _levelID++;
            _passedBaseCount++;
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onSaveAndResetGameData?.Invoke();
            MineInitialize();
            WorkerManagerInitialize();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        }

        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onSaveAndResetGameData?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();

        }

        private int OnGetLevelID()
        {
            return _levelID;
        }

        private void OnInitializeLevel()
        {
            UnityEngine.Object[] Levels = Resources.LoadAll("Levels");
            int newLevelId = _levelID % Levels.Length;
            levelLoader.InitializeLevel((GameObject)Levels[newLevelId], levelHolder.transform);

        }

        private void OnInitializeNextLevel()
        {
            UnityEngine.Object[] Levels = Resources.LoadAll("Levels");
            int nextLevelId = (_levelID + 1) % Levels.Length;
            levelLoader.InitializeLevel((GameObject)Levels[nextLevelId], levelHolder.transform, new Vector3(0, 0, 450f));

        }

        private void OnClearActiveLevel()
        {
            //levelClearer.ClearActiveLevel(levelHolder.transform);
        }
        private void InitializeAreas()
        {
            if (_openAreas.Equals(null))
            {
                return;
            }
            for (int i = 0; i < _openAreas.Count; i++)
            {
                AreaInstantiate(_levelModdedValue + 1, _openAreas[i]);
            }
        }

        private void InitializeTurrets()
        {
            if (_openTurrets.Equals(null))
            {
                return;
            }
            for (int i = 0; i < _openTurrets.Count; i++)
            {
                TurretInstantiate(_levelModdedValue + 1, _openTurrets[i]);
            }
        }

        private void OnBuyArea(int id)
        {
            AreaInstantiate(_levelModdedValue + 1, id);
        }

        private void AreaInstantiate(int levelId, int areaId)
        {
            areaLoader.InitializeLevel(levelId, areaId, levelHolder.transform.GetChild(_passedBaseCount));
        }
        private void OnBuyTurret(int id)
        {
            TurretInstantiate(_levelModdedValue + 1, id);
        }
        private void TurretInstantiate(int levelId, int turretId)
        {
            turretLoader.InitializeTurret(levelId, turretId, levelHolder.transform.GetChild(0));
        }

        private void MineInitialize()
        {
            Debug.Log(LevelModdedValue + 1);
            mineLoader.InitializeMine(LevelModdedValue + 1, levelHolder.transform.GetChild(0));
        }
        private void WorkerManagerInitialize()
        {
            workerLoader.InitializeWorkerManagers(levelHolder.transform.GetChild(_passedBaseCount).GetChild(0));
        }

        private int OnGetCurrentModdedLevel()
        {
            return _levelModdedValue = _levelID % Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;

        }

        private void OnBossDefeated()
        {
            Transform player = PlayerSignals.Instance.onGetPlayer();
            player.position = new Vector3(player.position.x, player.position.y, player.position.z - 450f);
            Vector3 oldBasePos = levelHolder.transform.GetChild(0).transform.position;
            levelHolder.transform.GetChild(0).transform.position = new Vector3(oldBasePos.x, oldBasePos.y, -450f);
            Vector3 newBase = levelHolder.transform.GetChild(1).transform.position;
            levelHolder.transform.GetChild(1).transform.position = new Vector3(newBase.x, newBase.y, 0f);
            

        }
    }
}