using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class RestoreGeneratorService : IPrimeSkillGeneratorService<RestoreParameterValueObject, RestoreValueObject>
    {
        private readonly IFactory<PlayerPropertyValueObject, CharacterTypeCode> _playerPropertyFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly PlayerDomainService _playerDomainService;

        public RestoreGeneratorService(
            IFactory<PlayerPropertyValueObject, CharacterTypeCode> playerPropertyFactory,
            OrderedItemsDomainService orderedItems,
            PlayerDomainService playerDomainService)
        {
            _playerPropertyFactory = playerPropertyFactory;
            _orderedItems = orderedItems;
            _playerDomainService = playerDomainService;
        }

        public IReadOnlyList<RestoreValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<RestoreParameterValueObject> restoreParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            MyDebug.Assert(restoreParameterList.Count == 1);
            _orderedItems.First().TryGetCharacterId(out var actorId);
            MyDebug.Assert(actorId != null);
            var currentTechnicalPoint = _playerDomainService.Get().CurrentTechnicalPoint;
            var maxTechnicalPoint = _playerPropertyFactory.Create(CharacterTypeCode.Player).TechnicalPoint;
            var restoreList = restoreParameterList.Select(GetRestore).ToList();
            return restoreList;
            
            RestoreValueObject GetRestore(RestoreParameterValueObject restoreParameter)
            {
                var technicalPoint = Math.Min(restoreParameter.TechnicalPoint, maxTechnicalPoint - currentTechnicalPoint);
                var restore =  new RestoreValueObject(
                    actorId: actorId,
                    skillCode: skillCommon.SkillCode,
                    targetIdList: targetIdList,
                    technicalPoint: technicalPoint);
                return restore;
            }
        }
    }
}