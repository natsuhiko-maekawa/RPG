using System.Collections.Immutable;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class RestoreGeneratorService
    {
        private readonly PlayerDomainService _playerDomainService;
        private readonly IRepository<TechnicalPointEntity, CharacterId> _technicalPointRepository;

        public RestoreGeneratorService(
            PlayerDomainService playerDomainService,
            IRepository<TechnicalPointEntity, CharacterId> technicalPointRepository)
        {
            _playerDomainService = playerDomainService;
            _technicalPointRepository = technicalPointRepository;
        }

        public RestoreValueObject Generate(
            SkillCommonValueObject skillCommon,
            RestoreParameterValueObject restoreParameter)
        {
            var playerId = _playerDomainService.GetId();
            var targetIdList = ImmutableList.Create(playerId);
            var technicalPoint = _technicalPointRepository.Select(playerId)
                .GetRestore(restoreParameter.TechnicalPoint);
            return new RestoreValueObject(
                actorId: playerId,
                skillCode: skillCommon.SkillCode,
                targetIdList: targetIdList,
                technicalPoint: technicalPoint);
        }
    }
}