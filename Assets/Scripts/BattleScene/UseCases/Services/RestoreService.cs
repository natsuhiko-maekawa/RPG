using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;
using Utility;

namespace BattleScene.UseCases.Services
{
    public class RestoreService : ISkillService<RestoreValueObject>
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

        public void UpdateBattleEvent(
            IReadOnlyList<BattleEventEntity> restoreEventList,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<RestoreValueObject> restoreList,
            IReadOnlyList<CharacterEntity> targetList)
        {
            MyDebug.Assert(restoreList.Count == 1);
            MyDebug.Assert(restoreEventList.Count == 1);
            var restoreEvent = restoreEventList.Single();
            // var actor = restoreEvent.Actor ?? throw new InvalidOperationException();
            var restore = restoreList.Single();

            var currentTechnicalPoint = _technicalPoint.Get();
            var maxTechnicalPoint = _playerPropertyFactory.Create(CharacterTypeCode.Player).TechnicalPoint;
            var technicalPoint = Math.Min(restore.TechnicalPoint, maxTechnicalPoint - currentTechnicalPoint);
            restoreEvent.UpdateRestore(
                technicalPoint: technicalPoint,
                targetList: targetList);
        }

        public void ExecuteBattleEvent(IReadOnlyList<BattleEventEntity> restoreEventList)
        {
            _technicalPoint.Restore(restoreEventList);
        }
    }
}