using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class ActualTargetIdPickerService
    {
        private const float Threshold = 40.0f; // 大きいほど命中しやすくなる
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IMyRandomService _myRandom;

        public ActualTargetIdPickerService(
            CharacterPropertyFactoryService characterPropertyFactory,
            OrderedItemsDomainService orderedItems,
            IMyRandomService myRandom)
        {
            _characterPropertyFactory = characterPropertyFactory;
            _orderedItems = orderedItems;
            _myRandom = myRandom;
        }

        public IReadOnlyList<CharacterId> Pick(
            IReadOnlyList<CharacterId> targetIdList,
            float luckRate = 1.0f)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var actorLuck = _characterPropertyFactory.Crate(actorId).Luck;

            var actualTargetList = targetIdList
                .Where(x =>
                {
                    var targetLuck = _characterPropertyFactory.Crate(x).Luck;
                    var rate = luckRate * (1.0f + (actorLuck - targetLuck) / Threshold);
                    return _myRandom.Probability(rate);
                })
                .ToImmutableList();

            return actualTargetList;
        }
    }
}