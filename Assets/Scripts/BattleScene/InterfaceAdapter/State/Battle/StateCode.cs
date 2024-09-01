using System;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public enum StateCode
    {
        NoState,
        [Obsolete]
        NoTrigger,
        Initialize,
        InitializeEnemy,
        Order,
        ResetAilment,
        SlipDamage,
        PlayerCantAction,
        EnemyCantAction,
        SelectAction,
        EnemySkill,
        ExecuteSkill,
        Damage,
        Ailment,
        DestroyedPart,
        Cure,
        Reset,
        Buff,
        LoopEnd
    }
}