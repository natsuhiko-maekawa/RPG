using System.Collections.Generic;

namespace BattleScene.Domain.Codes
{
    public enum SkillCode : byte
    {
        NoSkill,
        Afterimage,
        Attack,
        Bite,
        BiteOff,
        Bleeding,
        Burning,
        Confusion,
        CutUp,
        Defence,
        FieldRation,
        FirstAid,
        FlameThrow,
        Honzougaku,
        Ishinhou,
        Kuchiyose,
        Kyoukasuigetsu,
        Liquid,
        Metronome,
        Murasame,
        MusterStrength,
        Nadegiri,
        NumbLiquid,
        Onikoroshi,
        Paralysis,
        Poisoning,
        Punch,
        PutScythe,
        Raikiri,
        RandomShots,
        Shichishitou,
        SilverBullet,
        SmokeBomb,
        StarShell,
        Stringer,
        Suffocation,
        TaserGun,
        Utsusemi,
        Wabisuke
    }

    public static class SkillCodeList
    {
        public static IReadOnlyList<SkillCode> AilmentSkillCodeList => new[]
        {
            SkillCode.Confusion,
            SkillCode.Paralysis,
            SkillCode.Poisoning,
            SkillCode.Suffocation
        };
    }
}