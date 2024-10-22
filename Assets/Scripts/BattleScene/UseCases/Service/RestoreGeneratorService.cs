using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class RestoreGeneratorService : IPrimeSkillGeneratorService<RestoreParameterValueObject>
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly IFactory<PlayerPropertyValueObject, CharacterTypeCode> _playerPropertyFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly PlayerDomainService _playerDomainService;

        public RestoreGeneratorService(
            IFactory<PlayerPropertyValueObject, CharacterTypeCode> playerPropertyFactory,
            OrderedItemsDomainService orderedItems,
            PlayerDomainService playerDomainService,
            ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _playerPropertyFactory = playerPropertyFactory;
            _orderedItems = orderedItems;
            _playerDomainService = playerDomainService;
            _characterCollection = characterCollection;
        }

        public IReadOnlyList<PrimeSkillValueObject> Generate(
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

            PrimeSkillValueObject GetRestore(RestoreParameterValueObject restoreParameter)
            {
                var technicalPoint = Math.Min(restoreParameter.TechnicalPoint,
                    maxTechnicalPoint - currentTechnicalPoint);
                var restore = PrimeSkillValueObject.CreateRestore(
                    actorId: actorId,
                    skillCode: skillCommon.SkillCode,
                    targetIdList: targetIdList,
                    technicalPoint: technicalPoint);
                return restore;
            }
        }
        
        public void Register(IReadOnlyList<PrimeSkillValueObject> restoreList)
        {
            foreach (var restore in restoreList) AddTechnicalPoint(restore);
        }

        private void AddTechnicalPoint(PrimeSkillValueObject restore)
        {
            var currentTechnicalPoint = _characterCollection.Get(restore.ActorId!).CurrentTechnicalPoint;
            var technicalPoint = restore.TechnicalPoint;
            var newTechnicalPoint = currentTechnicalPoint + technicalPoint;
            _characterCollection.Get(restore.ActorId!).CurrentTechnicalPoint = newTechnicalPoint;
        }
    }
}