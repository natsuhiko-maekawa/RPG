﻿namespace BattleScene.UseCases.StateMachine
{
    public enum StateCode
    {
        NoTrigger,
        Initialize,
        ResetAilment,
        SlipDamage,
        PlayerCantAction,
        EnemyCantAction,
        SelectAction,
        EnemySkill,
        Damage,
        Ailment,
        DestroyedPart,
        Cure,
        Reset,
        Buff,
        LoopEnd
    }
}