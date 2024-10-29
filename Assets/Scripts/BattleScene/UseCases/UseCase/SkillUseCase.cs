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
        private readonly TechnicalPointService _technicalPoint;

        public SkillUseCase(
            TechnicalPointService technicalPoint, 
            ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _technicalPoint = technicalPoint;
            _characterCollection = characterCollection;
        }

        public void ExecuteSkill(CharacterId actorId, SkillValueObject skill)
        {
            if (_characterCollection.Get(actorId).IsPlayer)
            {
                _technicalPoint.Reduce(skill);
            }
        }
    }
}