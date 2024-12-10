using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    public interface IEnemySkillSelectorService
    {
        public SkillValueObject Select(CharacterEntity actor);
    }
}