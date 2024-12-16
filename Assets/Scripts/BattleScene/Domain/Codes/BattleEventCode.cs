namespace BattleScene.Domain.Codes
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