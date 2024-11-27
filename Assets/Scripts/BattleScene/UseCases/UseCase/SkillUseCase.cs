using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    public class SkillUseCase
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly ITechnicalPointService _technicalPoint;

        public SkillUseCase(
            BattleLoggerService battleLogger,
            ITechnicalPointService technicalPoint, 
            ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _battleLogger = battleLogger;
            _technicalPoint = technicalPoint;
            _characterCollection = characterCollection;
        }

        public void ExecuteSkill(CharacterId actorId, SkillValueObject skill)
        {
            var skillCode = skill.Common.SkillCode;
            _battleLogger.Log(skillCode);

            if (_characterCollection.Get(actorId).IsPlayer)
            {
                _technicalPoint.Reduce(skill);
            }
        }
    }
}