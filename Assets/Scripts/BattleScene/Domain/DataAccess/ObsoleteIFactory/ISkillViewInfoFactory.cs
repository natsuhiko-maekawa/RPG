using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.DataAccess.ObsoleteIFactory
{
    [Obsolete]
    public interface ISkillViewInfoFactory
    {
        public SkillViewInfoValueObject Create(SkillCode skillCode);
    }
}