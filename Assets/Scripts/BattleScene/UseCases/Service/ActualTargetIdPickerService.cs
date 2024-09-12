using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using Utility.Interface;

namespace BattleScene.UseCases.Service
{
    public class ActualTargetIdPickerService
    {
        private const float Threshold = 40.0f; // 大きいほど命中しやすくなる
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IRandomEx _randomEx;

        public ActualTargetIdPickerService(
            CharacterPropertyFactoryService characterPropertyFactory,
            OrderedItemsDomainService orderedItems,
            IRandomEx randomEx)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _orderedItems = orderedItems;
            _randomEx = randomEx;
        }

        public ImmutableList<CharacterId> Pick(
            IList<CharacterId> targetIdList,
            float luckRate = 1.0f)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var actorLuck = _characterPropertyFactory.Crate(actorId).Luck;

            var actualTargetList = targetIdList
                .Where(x =>
                {
                    var targetLuck = _characterPropertyFactory.Crate(x).Luck;
                    var rate = luckRate * (1.0f + (actorLuck - targetLuck) / Threshold);
                    return _randomEx.Probability(rate);
                })
                .ToImmutableList();

            return actualTargetList;
        }
    }
}