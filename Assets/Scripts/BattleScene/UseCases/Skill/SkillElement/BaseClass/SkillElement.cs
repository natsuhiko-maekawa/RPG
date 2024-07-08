using System;
using BattleScene.Domain.Interface;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement.BaseClass
{
    public abstract class SkillElement : ISkillElement
    {
        public int CompareTo(ISkillElement other)
        {
            return GetPriority(other) - GetPriority(this);
        }

        private int GetPriority(ISkillElement skillElement)
        {
            return skillElement switch
            {
                AbstractDamage => (int)SkillElementEnum.DamageSkillElement,
                AbstractAilment => (int)SkillElementEnum.AilmentSkillElement,
                AbstractSlipDamage => (int)SkillElementEnum.SlipDamageElement,
                AbstractDestroyPart => (int)SkillElementEnum.DestroyPartElement,
                AbstractBuff => (int)SkillElementEnum.BuffSkillElement,
                AbstractCure => (int)SkillElementEnum.CureSkillElement,
                AbstractReset => (int)SkillElementEnum.ResetSkillElement,
                AbstractRestoreTechnicalPoint => (int)SkillElementEnum.RestoreTechnicalPointElement,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public enum SkillElementEnum
    {
        DamageSkillElement,
        AilmentSkillElement,
        SlipDamageElement,
        DestroyPartElement,
        BuffSkillElement,
        CureSkillElement,
        ResetSkillElement,
        RestoreTechnicalPointElement,
    }
}