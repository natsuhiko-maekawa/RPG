namespace BattleScene.Domain.Code
{
    public enum BattleEventCode : byte
    {
        NoEvent,
        Attack,
        Skill,
        Defence,
        FatalitySkill,
        AilmentReset,
        SlipDamage,
        Continue,
        Quit
    }
}