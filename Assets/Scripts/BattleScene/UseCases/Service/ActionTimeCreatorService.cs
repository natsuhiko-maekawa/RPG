using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class ActionTimeCreatorService
    {
        private readonly IRepository<ActionTimeEntity, CharacterId> _actionTimeRepository;
        private readonly AgilityToSpeedService _agilityToSpeed;
        private readonly OrderedItemsDomainService _orderedItems;

        public ActionTimeCreatorService(
            IRepository<ActionTimeEntity, CharacterId> actionTimeRepository,
            AgilityToSpeedService agilityToSpeed,
            OrderedItemsDomainService orderedItems)
        {
            _actionTimeRepository = actionTimeRepository;
            _agilityToSpeed = agilityToSpeed;
            _orderedItems = orderedItems;
        }

        public ImmutableList<ActionTimeEntity> Create(IList<CharacterId> characterIdList)
        {
            if (_orderedItems.First().TryGetCharacterId(out var characterId))
                return ImmutableList<ActionTimeEntity>.Empty;

            return characterIdList
                .Select(x =>
                {
                    var actionTime = _actionTimeRepository.Select(x);
                    var minTime = characterIdList
                        .Select(y => _actionTimeRepository.Select(y).ActionTime)
                        .Min();
                    actionTime.Reduce(minTime);

                    if (!Equals(x, characterId))
                        return actionTime;

                    actionTime.Add(Constant.MaxAgility / _agilityToSpeed.Convert(x));
                    return actionTime;
                })
                .ToImmutableList();
        }

        public ImmutableList<ActionTimeEntity> CreateDefault(IList<CharacterId> characterIdList)
        {
            return characterIdList
                .Select(x => new ActionTimeEntity(x))
                .ToImmutableList();
        }
    }
}