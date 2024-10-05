using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using Utility;

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
            MyDebug.Assert(characterId != null);
            var ailmentCode = _ailment.GetHighestPriority(characterId)?.AilmentCode;
            MyDebug.Assert(ailmentCode.HasValue);
            // ReSharper disable once PossibleInvalidOperationException
            var skillCode = ailmentCode.Value switch
            {
                AilmentCode.Confusion => SkillCode.Confusion,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            return skillCode;
        }

        public IReadOnlyList<CharacterId> TargetIdList
        {
            get
            {
                _orderedItems.First().TryGetCharacterId(out var characterId);
                MyDebug.Assert(characterId != null);
                var targetIdList = new List<CharacterId> { characterId };
                return targetIdList;
            }
        }
    }
}