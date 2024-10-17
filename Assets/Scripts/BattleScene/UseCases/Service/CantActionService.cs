using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.Service
{
    public class CantActionService
    {
        private readonly AilmentDomainService _ailment;
        private readonly OrderedItemsDomainService _orderedItems;

        public CantActionService(
            AilmentDomainService ailment,
            OrderedItemsDomainService orderedItems)
        {
            _ailment = ailment;
            _orderedItems = orderedItems;
        }

        public SkillCode ToSkillCode()
        {
            _orderedItems.First().TryGetCharacterId(out var characterId);
            var ailmentCode = _ailment.GetHighestPriority(characterId)?.AilmentCode;
            if (!ailmentCode.HasValue) throw new InvalidOperationException();
            var skillCode = ailmentCode.Value switch
            {
                AilmentCode.Confusion => SkillCode.Confusion,
                AilmentCode.Paralysis => SkillCode.Paralysis,
                AilmentCode.EnemyParalysis => SkillCode.Paralysis,
                _ => throw new ArgumentOutOfRangeException(nameof(ailmentCode.Value), ailmentCode.Value, null)
            };

            return skillCode;
        }

        public IReadOnlyList<CharacterId> TargetIdList
        {
            get
            {
                _orderedItems.First().TryGetCharacterId(out var characterId);
                var targetIdList = new List<CharacterId> { characterId };
                return targetIdList;
            }
        }
    }
}