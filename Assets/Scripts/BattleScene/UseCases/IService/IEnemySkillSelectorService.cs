using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;

namespace BattleScene.UseCases.IService
{
    public interface IEnemySkillSelectorService
    {
        public SkillValueObject Select(CharacterEntity actor);
    }
}