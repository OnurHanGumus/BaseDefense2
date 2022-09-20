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
    public class SaveManager2 : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables


        #endregion

        #region Private Variables

        [ShowInInspector] private int _levelID;
        private LoadGameCommand _loadGameCommand;
        private SaveGameCommand _saveGameCommand;
        private List<int> _areaData, _turretAreaData, _turretOwnerData, _enemyAreaData, _gunLevelData;



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
            _areaData = GetAreaData();
            _turretAreaData = GetTurretAreaData();
            _turretOwnerData = GetTurretOwnerAreaData();
            _enemyAreaData = GetEnemyAreaData();
            _gunLevelData = GetGunLevelData();

            WriteSavesToScriptable(SaveLoadStates.OpenedAreasCounts, ref _areaData);
            WriteSavesToScriptable(SaveLoadStates.OpenedTurretsCounts, ref _turretAreaData);
            WriteSavesToScriptable(SaveLoadStates.OpenedTurretOwnersCounts, ref _turretOwnerData);
            WriteSavesToScriptable(SaveLoadStates.OpenedEnemyAreaCounts, ref _enemyAreaData);
            WriteSavesToScriptable(SaveLoadStates.GunLevels, ref _gunLevelData);

            SendCollectablesInformation();
        }

        public List<int> GetAreaData() => Resources.Load<CD_Area>("Data/Counts/CD_Area").totalLevelAreaData.Base[LevelSignals.Instance.onGetCurrentModdedLevel()].UnlockValues;
        public List<int> GetTurretAreaData() => Resources.Load<CD_Area>("Data/Counts/CD_Turret").totalLevelAreaData.Base[LevelSignals.Instance.onGetCurrentModdedLevel()].UnlockValues;
        public List<int> GetTurretOwnerAreaData() => Resources.Load<CD_Area>("Data/Counts/CD_TurretOwner").totalLevelAreaData.Base[LevelSignals.Instance.onGetCurrentModdedLevel()].UnlockValues;
        public List<int> GetEnemyAreaData() => Resources.Load<CD_Area>("Data/Counts/CD_EnemyArea").totalLevelAreaData.Base[LevelSignals.Instance.onGetCurrentModdedLevel()].UnlockValues;
        //public AllGunsData GetGunLevelData() => Resources.Load<CD_Gun>("Data/CD_Gun").Data;
        public List<int> GetGunLevelData() => Resources.Load<CD_Gun>("Data/CD_Gun").Levels;


        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onBuyArea += OnBuyArea;
            LevelSignals.Instance.onBuyEnemyArea += OnBuyEnemyAreas;
            LevelSignals.Instance.onBuyTurret += OnBuyTurret;
            LevelSignals.Instance.onBuyTurretOwners += OnBuyTurretOwners;
            CoreGameSignals.Instance.onSaveGameData += OnSaveGameData;
            PlayerSignals.Instance.onPlayerLeaveBuyArea += SetSaveValues;
            PlayerSignals.Instance.onPlayerSelectGun += OnChangeGun;
            SaveSignals.Instance.onSaveCollectables += OnSaveCollectables;
            SaveSignals.Instance.onGetSelectedGun += OnGetSelectedGunId;
            SaveSignals.Instance.onUpgradePlayer += OnUpgradePlayer;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onBuyArea -= OnBuyArea;
            LevelSignals.Instance.onBuyEnemyArea -= OnBuyEnemyAreas;
            LevelSignals.Instance.onBuyTurret -= OnBuyTurret;
            LevelSignals.Instance.onBuyTurretOwners -= OnBuyTurretOwners;
            CoreGameSignals.Instance.onSaveGameData -= OnSaveGameData;
            PlayerSignals.Instance.onPlayerLeaveBuyArea -= SetSaveValues;
            PlayerSignals.Instance.onPlayerSelectGun -= OnChangeGun;
            SaveSignals.Instance.onSaveCollectables -= OnSaveCollectables;
            SaveSignals.Instance.onGetSelectedGun -= OnGetSelectedGunId;
            SaveSignals.Instance.onUpgradePlayer -= OnUpgradePlayer;




        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnBuyArea(int id)
        {
            _saveGameCommand.OnSaveList(SaveLoadStates.CurrentLevelOpenedAreas, id);
            SetSaveValues(SaveLoadStates.OpenedAreasCounts);
        }

        private void OnBuyTurret(int id)
        {
            _saveGameCommand.OnSaveList(SaveLoadStates.OpenedTurrets, id, SaveFiles.WorkerCurrentCounts.ToString());
            SetSaveValues(SaveLoadStates.OpenedTurretsCounts);

        }
        private void OnBuyTurretOwners(int id)
        {
            _saveGameCommand.OnSaveList(SaveLoadStates.OpenedTurretOwners, id, SaveFiles.WorkerCurrentCounts.ToString());
            SetSaveValues(SaveLoadStates.OpenedTurretOwnersCounts);

        }

        private void OnBuyEnemyAreas(int id)
        {
            _saveGameCommand.OnSaveList(SaveLoadStates.OpenedEnemyAreas, id, SaveFiles.WorkerCurrentCounts.ToString());
            SetSaveValues(SaveLoadStates.OpenedEnemyAreaCounts);

        }

        private void OnChangeGun(int id)
        {
            _saveGameCommand.OnSaveData(SaveLoadStates.GunId, id, SaveFiles.Guns.ToString());
        }

        private void OnUpgradeGuns(int id)
        {
            _saveGameCommand.OnSaveList(SaveLoadStates.GunLevels, id, SaveFiles.Guns.ToString());
            SetSaveValues(SaveLoadStates.OpenedEnemyAreaCounts);

        }

        private void OnUpgradePlayer(PlayerUpgrades playerUpgrades,int id)
        {
            _saveGameCommand.OnSaveData(SaveLoadStates.UpgradePlayerCapacity, id, playerUpgrades.ToString());
        }

        private void OnSaveCollectables(SaveLoadStates type, int amount)
        {
            _saveGameCommand.OnSaveData(type, amount);
        }

        private void OnSaveGameData()
        {
            _saveGameCommand.OnSaveData(SaveLoadStates.Level, _loadGameCommand.OnLoadGameData(SaveLoadStates.Level) + 1);
            _saveGameCommand.OnSaveData(SaveLoadStates.Money, _loadGameCommand.OnLoadGameData(SaveLoadStates.Money));
            _saveGameCommand.OnSaveData(SaveLoadStates.Gem, _loadGameCommand.OnLoadGameData(SaveLoadStates.Gem));

            _saveGameCommand.OnResetList(SaveLoadStates.CurrentLevelOpenedAreas);
            _saveGameCommand.OnResetList(SaveLoadStates.OpenedTurrets);
            _saveGameCommand.OnResetList(SaveLoadStates.OpenedTurretOwners);
            _saveGameCommand.OnResetList(SaveLoadStates.OpenedAreasCounts);
            _saveGameCommand.OnResetList(SaveLoadStates.OpenedTurretsCounts);
            _saveGameCommand.OnResetList(SaveLoadStates.OpenedTurretOwnersCounts);
            _saveGameCommand.OnResetList(SaveLoadStates.OpenedEnemyAreaCounts);
        }
        //Area
        private void WriteSavesToScriptable(SaveLoadStates type, ref List<int> scriptableData)
        {
            int[] _saveArray = _loadGameCommand.OnLoadArray(type);
            bool isFirstInitialize = false;
            if (_saveArray[0].Equals(-1))
            {
                isFirstInitialize = true;
            }
            if (isFirstInitialize)
            {
                Debug.Log("First time Initialize");
                SetSaveValues(type);
            }
            else
            {
                SetScriptableValues(_saveArray, ref scriptableData);
            }

        }
        private void SetScriptableValues(int[] saveArray, ref List<int> scriptableData)
        {
            for (int i = 0; i < scriptableData.Count; i++)
            {
                scriptableData[i] = saveArray[i];
            }
        }

        private void SetSaveValues(SaveLoadStates type)
        {
            List<int> selectedData = new List<int>();

            if (type.Equals(SaveLoadStates.OpenedAreasCounts))
            {
                selectedData = _areaData;
            }
            else if (type.Equals(SaveLoadStates.OpenedTurretsCounts))
            {
                selectedData = _turretAreaData;
            }
            else if (type.Equals(SaveLoadStates.OpenedTurretOwnersCounts))
            {
                selectedData = _turretOwnerData;
            }
            else if (type.Equals(SaveLoadStates.OpenedEnemyAreaCounts))
            {
                selectedData = _enemyAreaData;
            }
            else if (type.Equals(SaveLoadStates.GunLevels))
            {
                selectedData = _gunLevelData;
            }
            else
            {

            }


            int[] temp = new int[selectedData.Count];
            for (int i = 0; i < selectedData.Count; i++)
            {
                temp[i] = selectedData[i];
            }
            _saveGameCommand.OnSaveArray(type, temp);
        }


        //Store



        private void SendCollectablesInformation()
        {
            SaveSignals.Instance.onInitializeSetMoney?.Invoke(_loadGameCommand.OnLoadGameData(SaveLoadStates.Money));
            SaveSignals.Instance.onInitializeSetGem?.Invoke(_loadGameCommand.OnLoadGameData(SaveLoadStates.Gem));
        }

        private int OnGetSelectedGunId()
        {
            Debug.Log(_loadGameCommand.OnLoadGameData(SaveLoadStates.GunId));
            return _loadGameCommand.OnLoadGameData(SaveLoadStates.GunId);
        }
    }
}