using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    public interface IEnemySkillSelectorService
    {
        [Obsolete]
        public SkillCode Select();
        public SkillValueObject Select(CharacterId actorId);
    }
}