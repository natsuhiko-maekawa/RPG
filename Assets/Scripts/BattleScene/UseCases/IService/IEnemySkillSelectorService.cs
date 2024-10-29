using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    public interface IEnemySkillSelectorService
    {
        public SkillValueObject Select(CharacterId actorId);
    }
}