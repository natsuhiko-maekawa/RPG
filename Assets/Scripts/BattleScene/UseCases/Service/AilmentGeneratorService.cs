using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Dto;
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
            PrimeSkillParameterDto<AilmentParameterValueObject> ailmentParameterDto)
        {
            if (!_orderedItems.First().TryGetCharacterId(out var actorId)) throw new InvalidOperationException();
            
            var ailmentList = ailmentParameterDto.List.Select(GetAilment).ToList();
            
            return ailmentList;
            
            AilmentValueObject GetAilment(AilmentParameterValueObject ailmentParameter)
            {
                var actualTargetIdList = GetActualTargetIdList(
                    targetIdList: ailmentParameterDto.TargetIdList,
                    ailmentParameter: ailmentParameter);
            
                var ailment = new AilmentValueObject(
                    actorId: actorId,
                    skillCode: ailmentParameterDto.SkillCommon.SkillCode,
                    ailmentCode: ailmentParameter.AilmentCode,
                    targetIdList: ailmentParameterDto.TargetIdList,
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