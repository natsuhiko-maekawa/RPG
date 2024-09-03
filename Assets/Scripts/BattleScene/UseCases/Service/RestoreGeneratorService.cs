using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class RestoreGeneratorService
    {
        private readonly PlayerDomainService _playerDomainService;

        public RestoreGeneratorService(
            PlayerDomainService playerDomainService)
        {
            _playerDomainService = playerDomainService;
        }

        public RestoreValueObject Generate(
            SkillCommonValueObject skillCommon,
            RestoreParameterValueObject restoreParameter)
        {
            var playerId = _playerDomainService.GetId();
            var targetIdList = ImmutableList.Create(playerId);
            return new RestoreValueObject(
                actorId: playerId,
                skillCode: skillCommon.SkillCode,
                targetIdList: targetIdList,
                technicalPoint: restoreParameter.TechnicalPoint);
        }
    }
}