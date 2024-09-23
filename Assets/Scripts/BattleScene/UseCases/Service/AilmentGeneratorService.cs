using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using Utility.Interface;

namespace BattleScene.UseCases.Service
{
    public class AilmentGeneratorService
    {
        private const float Threshold = 40.0f; // 大きいほど命中しやすくなる
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IRandomEx _randomEx;

        public AilmentGeneratorService(
            CharacterPropertyFactoryService characterPropertyFactory,
            OrderedItemsDomainService orderedItems,
            IRandomEx randomEx)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _orderedItems = orderedItems;
            _randomEx = randomEx;
        }

        public IReadOnlyList<AilmentValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<AilmentParameterValueObject> ailmentParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            if (!_orderedItems.First().TryGetCharacterId(out var actorId)) throw new InvalidOperationException();
            
            var ailmentList = ailmentParameterList.Select(GetAilment).ToList();
            
            return ailmentList;
            
            AilmentValueObject GetAilment(AilmentParameterValueObject ailmentParameter)
            {
                var actualTargetIdList = GetActualTargetIdList(
                    targetIdList: targetIdList,
                    ailmentParameter: ailmentParameter);
            
                var ailment = new AilmentValueObject(
                    actorId: actorId,
                    skillCode: skillCommon.SkillCode,
                    ailmentCode: ailmentParameter.AilmentCode,
                    targetIdList: targetIdList,
                    actualTargetIdList: actualTargetIdList);

                return ailment;
            }
        }
        
        // TODO: ActualTargetIdPickerService.Pick()に置き換える
        private IReadOnlyList<CharacterId> GetActualTargetIdList(
            IReadOnlyList<CharacterId> targetIdList,
            AilmentParameterValueObject ailmentParameter)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var actorLuck = _characterPropertyFactory.Crate(actorId).Luck;

            var actualTargetList = targetIdList
                .Where(x =>
                {
                    var targetLuck = _characterPropertyFactory.Crate(x).Luck;
                    var rate = ailmentParameter.LuckRate * (1.0f + (actorLuck - targetLuck) / Threshold);
                    return _randomEx.Probability(rate);
                })
                .ToImmutableList();

            return actualTargetList;
        }
    }
}