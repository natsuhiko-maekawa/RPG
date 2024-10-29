using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    public class SkillUseCase
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly SkillExecutorService _skillExecutor;

        public SkillUseCase(
            SkillExecutorService skillExecutor, 
            ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _skillExecutor = skillExecutor;
            _characterCollection = characterCollection;
        }

        public void ExecuteSkill(CharacterId actorId, SkillValueObject skill)
        {
            if (_characterCollection.Get(actorId).IsPlayer)
            {
                _skillExecutor.Execute(skill);
            }
        }
    }
}