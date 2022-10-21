using System;
using System.Collections.Generic;
using Commands;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Enums;

namespace Managers
{
    public class SaveManager2 : MonoBehaviour
    {
        #region Self Variables
        #region Private Variables

        [ShowInInspector] private int _levelID;
        private LoadGameCommand _loadGameCommand;
        private SaveGameCommand _saveGameCommand;

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

            SendCollectablesInformation();
            SendPlayerUpgradesInformation();
            SendGunLevelsInformation();
            SendSelectedGunIdInformation();
            SendWorkerUpgradesInformation();
        }

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
            LevelSignals.Instance.onMinerCountIncreased += OnIncreaseMinerCount;
            LevelSignals.Instance.onGetMinerCount += OnGetMinerCount;
            LevelSignals.Instance.onSoldierCountIncreased += OnIncreaseSoldierCount;
            LevelSignals.Instance.onGetSoldierCount += OnGetSoldierCount;
            CoreGameSignals.Instance.onSaveAndResetGameData += OnSaveGameData;
            PlayerSignals.Instance.onPlayerLeaveBuyArea += SetNewSaveAreaValue;
            PlayerSignals.Instance.onPlayerSelectGun += OnChangeGun;
            SaveSignals.Instance.onSaveCollectables += OnSaveCollectables;
            SaveSignals.Instance.onGetSelectedGun += OnGetSelectedGunId;
            SaveSignals.Instance.onUpgradePlayer += OnUpgradePlayer;
            SaveSignals.Instance.onUpgradeWorker += OnUpgradeWorker;
            SaveSignals.Instance.onGetOpenedTurrets += OnGetOpenedTurrets;
            SaveSignals.Instance.onGetWorkerUpgrades += OnGetWorkerUpgrades;
            SaveSignals.Instance.onGetBossHealth += OnGetBossHealth;
            SaveSignals.Instance.onBossTakedDamage += OnBossTakedDamage;
            UISignals.Instance.onChangeGunLevels += OnUpgradeGuns;
            UISignals.Instance.onGetGunLevels += OnGetGunLevels;
            LevelSignals.Instance.onGetAreasCount += OnGetAreaCounts;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onBuyArea -= OnBuyArea;
            LevelSignals.Instance.onBuyEnemyArea -= OnBuyEnemyAreas;
            LevelSignals.Instance.onBuyTurret -= OnBuyTurret;
            LevelSignals.Instance.onBuyTurretOwners -= OnBuyTurretOwners;
            LevelSignals.Instance.onMinerCountIncreased -= OnIncreaseMinerCount;
            LevelSignals.Instance.onGetMinerCount -= OnGetMinerCount;
            LevelSignals.Instance.onSoldierCountIncreased -= OnIncreaseSoldierCount;
            LevelSignals.Instance.onGetSoldierCount -= OnGetSoldierCount;
            CoreGameSignals.Instance.onSaveAndResetGameData -= OnSaveGameData;
            PlayerSignals.Instance.onPlayerLeaveBuyArea -= SetNewSaveAreaValue;
            PlayerSignals.Instance.onPlayerSelectGun -= OnChangeGun;
            SaveSignals.Instance.onSaveCollectables -= OnSaveCollectables;
            SaveSignals.Instance.onGetSelectedGun -= OnGetSelectedGunId;
            SaveSignals.Instance.onUpgradePlayer -= OnUpgradePlayer;
            SaveSignals.Instance.onUpgradeWorker -= OnUpgradeWorker;
            SaveSignals.Instance.onGetWorkerUpgrades -= OnGetWorkerUpgrades;
            SaveSignals.Instance.onGetOpenedTurrets -= OnGetOpenedTurrets;
            SaveSignals.Instance.onGetBossHealth -= OnGetBossHealth;
            SaveSignals.Instance.onBossTakedDamage -= OnBossTakedDamage;
            UISignals.Instance.onChangeGunLevels -= OnUpgradeGuns;
            UISignals.Instance.onGetGunLevels -= OnGetGunLevels;
            LevelSignals.Instance.onGetAreasCount -= OnGetAreaCounts;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnBuyArea(int id)
        {
            _saveGameCommand.OnSaveListAddElement(SaveLoadStates.CurrentLevelOpenedAreas, id);
        }

        private void OnBuyTurret(int id)
        {
            _saveGameCommand.OnSaveListAddElement(SaveLoadStates.OpenedTurrets, id, SaveFiles.WorkerCurrentCounts.ToString());

        }
        private void OnBuyTurretOwners(int id)
        {
            _saveGameCommand.OnSaveListAddElement(SaveLoadStates.OpenedTurretOwners, id, SaveFiles.WorkerCurrentCounts.ToString());

        }

        private void OnBuyEnemyAreas(int id)
        {
            _saveGameCommand.OnSaveListAddElement(SaveLoadStates.OpenedEnemyAreas, id, SaveFiles.WorkerCurrentCounts.ToString());

        }

        private void OnChangeGun(int id)
        {
            _saveGameCommand.OnSaveData(SaveLoadStates.GunId, id, SaveFiles.Guns.ToString());
        }

        private void OnUpgradeGuns(List<int> listToSave)
        {
            _saveGameCommand.OnSaveList(SaveLoadStates.GunLevels, listToSave, SaveFiles.Guns.ToString());
        }

        private void OnUpgradePlayer(List<int> listToSave)
        {
            _saveGameCommand.OnSaveList(SaveLoadStates.PlayerUpgrades, listToSave, SaveFiles.PlayerImprovements.ToString());
        }
        private void OnUpgradeWorker(List<int> listToSave)
        {
            _saveGameCommand.OnSaveList(SaveLoadStates.WorkerUpgrades, listToSave, SaveFiles.WorkerUpgrades.ToString());
        }

        private void OnSaveCollectables(SaveLoadStates type, int amount)
        {
            _saveGameCommand.OnSaveData(type, amount);
        }

        private void OnIncreaseMinerCount(int increaseAmount)
        {
            int currentCount = _loadGameCommand.OnLoadGameData(SaveLoadStates.MinerCount, SaveFiles.WorkerCurrentCounts.ToString());
            _saveGameCommand.OnSaveData(SaveLoadStates.MinerCount, currentCount + increaseAmount, SaveFiles.WorkerCurrentCounts.ToString());
        }
        private void OnIncreaseSoldierCount(int newValue)
        {
            _saveGameCommand.OnSaveData(SaveLoadStates.SoldierCount, newValue, SaveFiles.WorkerCurrentCounts.ToString());
        }
        private void OnBossTakedDamage(int newValue)
        {
            _saveGameCommand.OnSaveData(SaveLoadStates.BossHealth, newValue, SaveFiles.SaveFile.ToString());
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
            _saveGameCommand.OnResetArray(SaveLoadStates.MoneyWorkerAreaCounts, SaveFiles.WorkerCurrentCounts.ToString());

            _saveGameCommand.OnSaveData(SaveLoadStates.MinerCount, 0, SaveFiles.WorkerCurrentCounts.ToString());
            _saveGameCommand.OnSaveData(SaveLoadStates.SoldierCount, 0, SaveFiles.WorkerCurrentCounts.ToString());
            _saveGameCommand.OnSaveData(SaveLoadStates.BossHealth, 0, SaveFiles.SaveFile.ToString());
        }

        private void SetNewSaveAreaValue(SaveLoadStates type, int[] newArray)
        {
            _saveGameCommand.OnSaveArray(type, newArray, SaveFiles.WorkerCurrentCounts.ToString());
        }
        private void SendCollectablesInformation()
        {
            SaveSignals.Instance.onInitializeSetMoney?.Invoke(_loadGameCommand.OnLoadGameData(SaveLoadStates.Money));
            SaveSignals.Instance.onInitializeSetGem?.Invoke(_loadGameCommand.OnLoadGameData(SaveLoadStates.Gem));
        }
        private void SendPlayerUpgradesInformation()
        {
            List<int> temp = _loadGameCommand.OnLoadList(SaveLoadStates.PlayerUpgrades, SaveFiles.PlayerImprovements.ToString());


            SaveSignals.Instance.onInitializePlayerUpgrades?.Invoke(temp);
        }
        private void SendWorkerUpgradesInformation()
        {
            List<int> temp = _loadGameCommand.OnLoadList(SaveLoadStates.WorkerUpgrades, SaveFiles.WorkerUpgrades.ToString());

            SaveSignals.Instance.onInitializeWorkerUpgrades?.Invoke(temp);
        }
        private void SendGunLevelsInformation()
        {
            UISignals.Instance.onInitializeGunLevels?.Invoke(_loadGameCommand.OnLoadList(SaveLoadStates.GunLevels, SaveFiles.Guns.ToString()));
        }

        private void SendSelectedGunIdInformation()
        {
            SaveSignals.Instance.onInitializeSelectedGunId?.Invoke(_loadGameCommand.OnLoadGameData(SaveLoadStates.GunId, SaveFiles.Guns.ToString()));
        }

        private int OnGetSelectedGunId()
        {
            return _loadGameCommand.OnLoadGameData(SaveLoadStates.GunId, SaveFiles.Guns.ToString());
        }
        private int OnGetMinerCount()
        {
            return _loadGameCommand.OnLoadGameData(SaveLoadStates.MinerCount, SaveFiles.WorkerCurrentCounts.ToString());
        }
        private int OnGetSoldierCount()
        {
            return _loadGameCommand.OnLoadGameData(SaveLoadStates.SoldierCount, SaveFiles.WorkerCurrentCounts.ToString());
        }
        private List<int> OnGetGunLevels()
        {
            return _loadGameCommand.OnLoadList(SaveLoadStates.GunLevels, SaveFiles.Guns.ToString());
        }
        private List<int> OnGetOpenedTurrets()
        {
            return _loadGameCommand.OnLoadList(SaveLoadStates.OpenedTurrets, SaveFiles.WorkerCurrentCounts.ToString());

        }
        private List<int> OnGetWorkerUpgrades()
        {
            return _loadGameCommand.OnLoadList(SaveLoadStates.WorkerUpgrades, SaveFiles.WorkerUpgrades.ToString());

        }

        private int[] OnGetAreaCounts(SaveLoadStates saveType)
        {
            return _loadGameCommand.OnLoadArray(saveType, SaveFiles.WorkerCurrentCounts.ToString());
        }
        private int OnGetBossHealth()
        {
            return _loadGameCommand.OnLoadGameData(SaveLoadStates.BossHealth, SaveFiles.SaveFile.ToString());
        }
    }
}