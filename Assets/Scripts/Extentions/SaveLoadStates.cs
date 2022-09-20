using System;

namespace Enums
{
    [Serializable]
    public enum SaveLoadStates
    {
        Level,
        CurrentBossHealth,
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

    }
}