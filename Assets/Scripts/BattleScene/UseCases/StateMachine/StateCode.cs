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
        EnemyAttack,
        Damage,
        Ailment,
        DestroyedPart,
        Cure,
        Reset,
        Buff,
        LoopEnd
    }
}