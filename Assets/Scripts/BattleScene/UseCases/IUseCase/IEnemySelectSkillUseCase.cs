using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IUseCase
{
    public interface IEnemySelectSkillUseCase
    {
        public SkillValueObject SelectSkill(CharacterId actorId);
        public IReadOnlyList<CharacterId> SelectTarget(CharacterId actorId, SkillValueObject skill);
    }
}