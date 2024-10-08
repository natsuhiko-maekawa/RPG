using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class AilmentGeneratorService : IPrimeSkillGeneratorService<AilmentParameterValueObject, AilmentValueObject>
    {
        private const float Threshold = 40.0f; // 大きいほど命中しやすくなる
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IMyRandomService _myRandom;

        public AilmentGeneratorService(
            CharacterPropertyFactoryService characterPropertyFactory,
            OrderedItemsDomainService orderedItems,
            IMyRandomService myRandom)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _orderedItems = orderedItems;
            _myRandom = myRandom;
        }

        public IReadOnlyList<AilmentValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<AilmentParameterValueObject> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            if (!_orderedItems.First().TryGetCharacterId(out var actorId)) throw new InvalidOperationException();
            
            var ailmentList = primeSkillParameterList.Select(GetAilment).ToList();
            
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
            var actorLuck = _characterPropertyFactory.Create(actorId).Luck;

            var actualTargetList = targetIdList
                .Where(x =>
                {
                    var targetLuck = _characterPropertyFactory.Create(x).Luck;
                    var rate = ailmentParameter.LuckRate * (1.0f + (actorLuck - targetLuck) / Threshold);
                    return _myRandom.Probability(rate);
                })
                .ToImmutableList();

            return actualTargetList;
        }
    }
}