using System;

namespace BattleScene.UseCases.StateMachine
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