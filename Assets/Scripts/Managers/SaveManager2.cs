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
        private List<int> _areaData, _turretAreaData, _turretOwnerData, _enemyAreaData, _gunLevelData, _ammoWorkerData, _moneyWorkerData;



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
            _ammoWorkerData = GetAmmoWorkerData();
            _moneyWorkerData = GetMoneyWorkerData();
            //_gunLevelData = GetGunLevelData();

            WriteSavesToScriptable(SaveLoadStates.OpenedAreasCounts, ref _areaData, SaveFiles.WorkerCurrentCounts);
            WriteSavesToScriptable(SaveLoadStates.OpenedTurretsCounts, ref _turretAreaData, SaveFiles.WorkerCurrentCounts);
            WriteSavesToScriptable(SaveLoadStates.OpenedTurretOwnersCounts, ref _turretOwnerData, SaveFiles.WorkerCurrentCounts);
            WriteSavesToScriptable(SaveLoadStates.OpenedEnemyAreaCounts, ref _enemyAreaData, SaveFiles.WorkerCurrentCounts);
            WriteSavesToScriptable(SaveLoadStates.AmmoWorkerAreaCounts, ref _ammoWorkerData, SaveFiles.WorkerCurrentCounts);
            WriteSavesToScriptable(SaveLoadStates.MoneyWorkerAreaCounts, ref _moneyWorkerData, SaveFiles.WorkerCurrentCounts);
            //WriteSavesToScriptable(SaveLoadStates.GunLevels, ref _gunLevelData, SaveFiles.Guns);

            SendCollectablesInformation();
            SendPlayerUpgradesInformation();
            SendGunLevelsInformation();
            SendSelectedGunIdInformation();
            SendOpenedTurretsInformation();
        }


        public List<int> GetAreaData() => Resources.Load<CD_Area>("Data/Counts/CD_Area").totalLevelAreaData.Base[LevelSignals.Instance.onGetCurrentModdedLevel()].UnlockValues;
        public List<int> GetTurretAreaData() => Resources.Load<CD_Area>("Data/Counts/CD_Turret").totalLevelAreaData.Base[LevelSignals.Instance.onGetCurrentModdedLevel()].UnlockValues;
        public List<int> GetTurretOwnerAreaData() => Resources.Load<CD_Area>("Data/Counts/CD_TurretOwner").totalLevelAreaData.Base[LevelSignals.Instance.onGetCurrentModdedLevel()].UnlockValues;
        public List<int> GetEnemyAreaData() => Resources.Load<CD_Area>("Data/Counts/CD_EnemyArea").totalLevelAreaData.Base[LevelSignals.Instance.onGetCurrentModdedLevel()].UnlockValues;
        public List<int> GetAmmoWorkerData() => Resources.Load<CD_Area>("Data/Counts/CD_AmmoWorker").totalLevelAreaData.Base[LevelSignals.Instance.onGetCurrentModdedLevel()].UnlockValues;
        public List<int> GetMoneyWorkerData() => Resources.Load<CD_Area>("Data/Counts/CD_MoneyWorker").totalLevelAreaData.Base[LevelSignals.Instance.onGetCurrentModdedLevel()].UnlockValues;
        //public AllGunsData GetGunLevelData() => Resources.Load<CD_Gun>("Data/CD_Gun").Data;
        //public List<int> GetGunLevelData() => Resources.Load<CD_Gun>("Data/CD_Gun").Levels;


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
            SaveSignals.Instance.onIncreaseAmmoWorkerCount += OnIncreaseWorkerCount;
            UISignals.Instance.onChangeGunLevels += OnUpgradeGuns;
            UISignals.Instance.onGetGunLevels += OnGetGunLevels;

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
            SaveSignals.Instance.onIncreaseAmmoWorkerCount -= OnIncreaseWorkerCount;
            UISignals.Instance.onChangeGunLevels -= OnUpgradeGuns;
            UISignals.Instance.onGetGunLevels -= OnGetGunLevels;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnBuyArea(int id)
        {
            _saveGameCommand.OnSaveListAddElement(SaveLoadStates.CurrentLevelOpenedAreas, id);
            SetSaveValues(SaveLoadStates.OpenedAreasCounts);
        }

        private void OnBuyTurret(int id)
        {
            _saveGameCommand.OnSaveListAddElement(SaveLoadStates.OpenedTurrets, id, SaveFiles.WorkerCurrentCounts.ToString());
            SetSaveValues(SaveLoadStates.OpenedTurretsCounts);

        }
        private void OnBuyTurretOwners(int id)
        {
            _saveGameCommand.OnSaveListAddElement(SaveLoadStates.OpenedTurretOwners, id, SaveFiles.WorkerCurrentCounts.ToString());
            SetSaveValues(SaveLoadStates.OpenedTurretOwnersCounts);

        }

        private void OnBuyEnemyAreas(int id)
        {
            _saveGameCommand.OnSaveListAddElement(SaveLoadStates.OpenedEnemyAreas, id, SaveFiles.WorkerCurrentCounts.ToString());
            SetSaveValues(SaveLoadStates.OpenedEnemyAreaCounts);

        }

        private void OnChangeGun(int id)
        {
            _saveGameCommand.OnSaveData(SaveLoadStates.GunId, id, SaveFiles.Guns.ToString());
        }

        private void OnUpgradeGuns(List<int> listToSave)
        {
            _saveGameCommand.OnSaveList(SaveLoadStates.GunLevels, listToSave, SaveFiles.Guns.ToString());
            //SetSaveValues(SaveLoadStates.OpenedEnemyAreaCounts);

        }

        private void OnUpgradePlayer(List<int> listToSave)
        {
            _saveGameCommand.OnSaveList(SaveLoadStates.PlayerUpgrades, listToSave, SaveFiles.PlayerImprovements.ToString());
        }

        private void OnSaveCollectables(SaveLoadStates type, int amount)
        {
            _saveGameCommand.OnSaveData(type, amount);
        }

        private void OnIncreaseWorkerCount(SaveLoadStates type)
        {
            int temp = _loadGameCommand.OnLoadGameData(SaveLoadStates.AmmoWorkerCounts, SaveFiles.WorkerCurrentCounts.ToString());
            _saveGameCommand.OnSaveData(type, temp, SaveFiles.WorkerCurrentCounts.ToString());
        }
        private void OnSaveGameData()
        {
            _saveGameCommand.OnSaveData(SaveLoadStates.Level, _loadGameCommand.OnLoadGameData(SaveLoadStates.Level) + 1);
            _saveGameCommand.OnSaveData(SaveLoadStates.Money, _loadGameCommand.OnLoadGameData(SaveLoadStates.Money));
            _saveGameCommand.OnSaveData(SaveLoadStates.Gem, _loadGameCommand.OnLoadGameData(SaveLoadStates.Gem));

            _saveGameCommand.OnResetList(SaveLoadStates.CurrentLevelOpenedAreas);
            _saveGameCommand.OnResetList(SaveLoadStates.OpenedTurrets, SaveFiles.WorkerCurrentCounts.ToString());
            _saveGameCommand.OnResetList(SaveLoadStates.OpenedTurretOwners, SaveFiles.WorkerCurrentCounts.ToString());

            _saveGameCommand.OnResetArray(SaveLoadStates.OpenedAreasCounts, SaveFiles.WorkerCurrentCounts.ToString());
            _saveGameCommand.OnResetArray(SaveLoadStates.OpenedTurretsCounts, SaveFiles.WorkerCurrentCounts.ToString());
            _saveGameCommand.OnResetArray(SaveLoadStates.OpenedTurretOwnersCounts, SaveFiles.WorkerCurrentCounts.ToString());
            _saveGameCommand.OnResetArray(SaveLoadStates.OpenedEnemyAreaCounts, SaveFiles.WorkerCurrentCounts.ToString());

            _saveGameCommand.OnResetArray(SaveLoadStates.AmmoWorkerAreaCounts, SaveFiles.WorkerCurrentCounts.ToString());
        }
        //Area
        private void WriteSavesToScriptable(SaveLoadStates type, ref List<int> scriptableData, SaveFiles saveFile)
        {
            int[] _saveArray = _loadGameCommand.OnLoadArray(type, saveFile.ToString());
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
            SaveFiles fileType;

            if (type.Equals(SaveLoadStates.OpenedAreasCounts))
            {
                selectedData = _areaData;
                fileType = SaveFiles.WorkerCurrentCounts;
            }
            else if (type.Equals(SaveLoadStates.OpenedTurretsCounts))
            {
                selectedData = _turretAreaData;
                fileType = SaveFiles.WorkerCurrentCounts;

            }
            else if (type.Equals(SaveLoadStates.OpenedTurretOwnersCounts))
            {
                selectedData = _turretOwnerData;
                fileType = SaveFiles.WorkerCurrentCounts;

            }
            else if (type.Equals(SaveLoadStates.OpenedEnemyAreaCounts))
            {
                selectedData = _enemyAreaData;
                fileType = SaveFiles.WorkerCurrentCounts;

            }
            else if (type.Equals(SaveLoadStates.GunLevels))
            {
                selectedData = _gunLevelData;
                fileType = SaveFiles.Guns;

            }
            else if (type.Equals(SaveLoadStates.AmmoWorkerAreaCounts))
            {
                selectedData = _ammoWorkerData;
                fileType = SaveFiles.WorkerCurrentCounts;

            }
            else if (type.Equals(SaveLoadStates.MoneyWorkerAreaCounts))
            {
                selectedData = _moneyWorkerData;
                fileType = SaveFiles.WorkerCurrentCounts;

            }
            else
            {
                fileType = SaveFiles.SaveFile;

            }


            int[] temp = new int[selectedData.Count];
            for (int i = 0; i < selectedData.Count; i++)
            {
                temp[i] = selectedData[i];
            }
            _saveGameCommand.OnSaveArray(type, temp, fileType.ToString());
        }


        //Store



        private void SendCollectablesInformation()
        {
            SaveSignals.Instance.onInitializeSetMoney?.Invoke(_loadGameCommand.OnLoadGameData(SaveLoadStates.Money));
            SaveSignals.Instance.onInitializeSetGem?.Invoke(_loadGameCommand.OnLoadGameData(SaveLoadStates.Gem));
        }
        private void SendPlayerUpgradesInformation()
        {
            List<int> temp = _loadGameCommand.OnLoadList(SaveLoadStates.PlayerUpgrades, SaveFiles.PlayerImprovements.ToString());
            //SaveSignals.Instance.onInitializePlayerCapacity?.Invoke(temp[0]);
            //SaveSignals.Instance.onInitializePlayerSpeed?.Invoke(temp[1]);
            //SaveSignals.Instance.onInitializePlayerHealth?.Invoke(temp[2]);

            SaveSignals.Instance.onInitializePlayerUpgrades?.Invoke(temp);
        }
        private void SendGunLevelsInformation()
        {
            UISignals.Instance.onInitializeGunLevels?.Invoke(_loadGameCommand.OnLoadList(SaveLoadStates.GunLevels, SaveFiles.Guns.ToString()));
        }

        private void SendSelectedGunIdInformation()
        {
            SaveSignals.Instance.onInitializeSelectedGunId?.Invoke(_loadGameCommand.OnLoadGameData(SaveLoadStates.GunId, SaveFiles.Guns.ToString()));
        }
        private void SendOpenedTurretsInformation()
        {
            SaveSignals.Instance.onInitializeOpenedTurretInfo?.Invoke(_loadGameCommand.OnLoadList(Enums.SaveLoadStates.OpenedTurrets, SaveFiles.WorkerCurrentCounts.ToString()));
        }

        private int OnGetSelectedGunId()
        {
            return _loadGameCommand.OnLoadGameData(SaveLoadStates.GunId, SaveFiles.Guns.ToString());
        }
        private List<int> OnGetGunLevels()
        {
            return _loadGameCommand.OnLoadList(SaveLoadStates.GunLevels, SaveFiles.Guns.ToString());
        }
    }
}