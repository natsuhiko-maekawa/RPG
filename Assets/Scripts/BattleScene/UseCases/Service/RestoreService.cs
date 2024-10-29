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
    public class RestoreService : IPrimeSkillService<RestoreParameterValueObject>
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly IFactory<PlayerPropertyValueObject, CharacterTypeCode> _playerPropertyFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ITechnicalPointService _technicalPoint;

        public RestoreService(
            IFactory<PlayerPropertyValueObject, CharacterTypeCode> playerPropertyFactory,
            OrderedItemsDomainService orderedItems,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            ITechnicalPointService technicalPoint)
        {
            _playerPropertyFactory = playerPropertyFactory;
            _orderedItems = orderedItems;
            _characterCollection = characterCollection;
            _technicalPoint = technicalPoint;
        }

        public IReadOnlyList<BattleEventValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<RestoreParameterValueObject> restoreParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            MyDebug.Assert(restoreParameterList.Count == 1);
            _orderedItems.First().TryGetCharacterId(out var actorId);
            MyDebug.Assert(actorId != null);
            var currentTechnicalPoint = _technicalPoint.Get();
            var maxTechnicalPoint = _playerPropertyFactory.Create(CharacterTypeCode.Player).TechnicalPoint;
            var restoreList = restoreParameterList.Select(GetRestore).ToList();
            return restoreList;

            BattleEventValueObject GetRestore(RestoreParameterValueObject restoreParameter)
            {
                var technicalPoint = Math.Min(restoreParameter.TechnicalPoint,
                    maxTechnicalPoint - currentTechnicalPoint);
                var restore = BattleEventValueObject.CreateRestore(
                    actorId: actorId,
                    skillCode: skillCommon.SkillCode,
                    targetIdList: targetIdList,
                    technicalPoint: technicalPoint);
                return restore;
            }
        }
        
        public void Register(IReadOnlyList<BattleEventValueObject> restoreList)
        {
            foreach (var restore in restoreList) AddTechnicalPoint(restore);
        }

        private void AddTechnicalPoint(BattleEventValueObject restore)
        {
            var currentTechnicalPoint = _characterCollection.Get(restore.ActorId!).CurrentTechnicalPoint;
            var technicalPoint = restore.TechnicalPoint;
            var newTechnicalPoint = currentTechnicalPoint + technicalPoint;
            _characterCollection.Get(restore.ActorId!).CurrentTechnicalPoint = newTechnicalPoint;
        }
    }
}