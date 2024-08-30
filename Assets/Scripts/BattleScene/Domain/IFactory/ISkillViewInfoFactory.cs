using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.IFactory
{
    [Obsolete]
    public interface ISkillViewInfoFactory
    {
        public SkillViewInfoValueObject Create(SkillCode skillCode);
    }
}