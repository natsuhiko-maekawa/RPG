using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class RestoreGeneratorService
    {
        private readonly IFactory<PlayerPropertyValueObject, CharacterTypeCode> _playerPropertyFactory;
        private readonly PlayerDomainService _playerDomainService;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public RestoreGeneratorService(
            IFactory<PlayerPropertyValueObject, CharacterTypeCode> playerPropertyFactory,
            PlayerDomainService playerDomainService,
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _playerPropertyFactory = playerPropertyFactory;
            _playerDomainService = playerDomainService;
            _characterRepository = characterRepository;
        }

        public RestoreValueObject Generate(
            SkillCommonValueObject skillCommon,
            RestoreParameterValueObject restoreParameter)
        {
            var playerId = _playerDomainService.GetId();
            var targetIdList = ImmutableList.Create(playerId);
            var currentTechnicalPoint = _characterRepository.Select(playerId).CurrentTechnicalPoint;
            var maxTechnicalPoint = _playerPropertyFactory.Create(CharacterTypeCode.Player).TechnicalPoint;
            var technicalPoint = Math.Min(restoreParameter.TechnicalPoint, maxTechnicalPoint - currentTechnicalPoint);
            return new RestoreValueObject(
                actorId: playerId,
                skillCode: skillCommon.SkillCode,
                targetIdList: targetIdList,
                technicalPoint: technicalPoint);
        }
    }
}