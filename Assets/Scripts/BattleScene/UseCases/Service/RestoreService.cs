using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class RestoreService : ISkillElementService<RestoreValueObject>
    {
        private readonly IFactory<PlayerPropertyValueObject, CharacterTypeCode> _playerPropertyFactory;
        private readonly ITechnicalPointService _technicalPoint;

        public RestoreService(
            IFactory<PlayerPropertyValueObject, CharacterTypeCode> playerPropertyFactory,
            ITechnicalPointService technicalPoint)
        {
            _playerPropertyFactory = playerPropertyFactory;
            _technicalPoint = technicalPoint;
        }

        // public IReadOnlyList<BattleEventEntity> GenerateBattleEvent(
        //     CharacterId actorId,
        //     SkillCommonValueObject skillCommon,
        //     IReadOnlyList<RestoreValueObject> restoreParameterList,
        //     IReadOnlyList<CharacterId> targetIdList)
        // {
        //     if (restoreParameterList.Count != 1)
        //         throw new InvalidOperationException(ExceptionMessage.RestoreParameterIsNoSingle);
        //     var currentTechnicalPoint = _technicalPoint.Get();
        //     var maxTechnicalPoint = _playerPropertyFactory.Create(CharacterTypeCode.Player).TechnicalPoint;
        //     var restoreList = restoreParameterList.Select(GetRestore).ToList();
        //     return restoreList;
        //     
        //     BattleEventValueObject GetRestore(RestoreValueObject restoreParameter)
        //     {
        //         var technicalPoint = Math.Min(restoreParameter.TechnicalPoint,
        //             maxTechnicalPoint - currentTechnicalPoint);
        //         var restore = BattleEventValueObject.CreateRestore(
        //             actorId: actorId,
        //             skillCode: skillCommon.SkillCode,
        //             targetIdList: targetIdList,
        //             technicalPoint: technicalPoint);
        //         return restore;
        //     }
        // }

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> restoreEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<RestoreValueObject> restoreList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            MyDebug.Assert(restoreList.Count == 1);
            MyDebug.Assert(restoreEventList.Count == 1);
            var restoreEvent = restoreEventList.Single();
            var actorId = restoreEvent.ActorId;
            MyDebug.Assert(actorId is not null);
            var restore = restoreList.Single();

            var currentTechnicalPoint = _technicalPoint.Get();
            var maxTechnicalPoint = _playerPropertyFactory.Create(CharacterTypeCode.Player).TechnicalPoint;
            var technicalPoint = Math.Min(restore.TechnicalPoint, maxTechnicalPoint - currentTechnicalPoint);
            restoreEvent.UpdateRestore(
                technicalPoint: technicalPoint,
                targetIdList: targetIdList);
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> restoreEventList)
        {
            _technicalPoint.Restore(restoreEventList);
        }
    }
}