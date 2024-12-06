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
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly ITechnicalPointService _technicalPoint;

        public SkillUseCase(
            BattleLoggerService battleLogger,
            ITechnicalPointService technicalPoint, 
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _battleLogger = battleLogger;
            _technicalPoint = technicalPoint;
            _characterRepository = characterRepository;
        }

        public void ExecuteSkill(CharacterId actorId, SkillValueObject skill)
        {
            var skillEvent = _battleLogger.GetLast();
            skillEvent.UpdateSkill(skill.Common.SkillCode);

            if (_characterRepository.Get(actorId).IsPlayer)
            {
                _technicalPoint.Reduce(skill);
            }
        }
    }
}