using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;
using BattleScene.UseCases.Services;

namespace BattleScene.UseCases.UseCases
{
    public class SkillUseCase
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly ITechnicalPointService _technicalPoint;

        public SkillUseCase(
            BattleLoggerService battleLogger,
            ITechnicalPointService technicalPoint)
        {
            _battleLogger = battleLogger;
            _technicalPoint = technicalPoint;
        }

        public void ExecuteSkill(CharacterEntity actor, SkillValueObject skill)
        {
            var skillEvent = _battleLogger.GetLast();
            skillEvent.UpdateSkill(skill.Common.SkillCode);

            if (actor.IsPlayer)
            {
                _technicalPoint.Reduce(skill);
            }
        }
    }
}