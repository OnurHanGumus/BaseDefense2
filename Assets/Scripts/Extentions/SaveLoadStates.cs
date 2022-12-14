using System;

namespace Enums
{
    [Serializable]
    public enum SaveLoadStates
    {
        Level,
        CurrentLevelOpenedAreas,
        Money,
        Gem,

        OpenedTurrets,
        OpenedTurretOwners,

        OpenedEnemyAreas,

        OpenedAreasCounts,
        OpenedTurretsCounts,
        OpenedTurretOwnersCounts,
        OpenedEnemyAreaCounts,

        GunId,
        GunLevels,

        UpgradePlayerCapacity,
        UpgradePlayerMoveSpeed,
        UpgradePlayerHealth,
        PlayerUpgrades,
        WorkerUpgrades,

        AmmoWorkerCounts,
        MoneyWorkerCounts,
        AmmoWorkerAreaCounts,
        MoneyWorkerAreaCounts,

        MinerCount,
        SoldierCount,

        BossHealth
    }
}