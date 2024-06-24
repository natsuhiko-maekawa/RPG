namespace BattleScene.UseCases.StateMachine
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
    }
}