namespace BattleScene.UseCases.StateMachine
{
    public enum StateCode
    {
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