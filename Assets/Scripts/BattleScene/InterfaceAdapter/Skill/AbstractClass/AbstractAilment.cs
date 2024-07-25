using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.InterfaceAdapter.Skill.AbstractClass
{
    public abstract class AbstractAilment : ILuckSkillElement
    {
        public abstract AilmentCode AilmentCode { get; }
        public virtual float LuckRate { get; } = 0.5f;
        
        [Obsolete]
        public virtual float GetLuckRate()
        {
            return 0.5f;
        }

        [Obsolete]
        public abstract AilmentCode GetAilmentCode();
    }
}