using System;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement.BaseClass
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
                DamageSkillElement => (int)SkillElementEnum.DamageSkillElement,
                AilmentSkillElement => (int)SkillElementEnum.AilmentSkillElement,
                SlipDamageElement => (int)SkillElementEnum.SlipDamageElement,
                DestroyPartSkillElement => (int)SkillElementEnum.DestroyPartElement,
                BuffSkillElement => (int)SkillElementEnum.BuffSkillElement,
                CureSkillElement => (int)SkillElementEnum.CureSkillElement,
                ResetSkillElement => (int)SkillElementEnum.ResetSkillElement,
                RestoreTechnicalPointSkillElement => (int)SkillElementEnum.RestoreTechnicalPointElement,
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