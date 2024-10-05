using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class SlipService
    {
        private readonly OrderedItemsDomainService _orderedItems;

        public SlipService(OrderedItemsDomainService orderedItems)
        {
            _orderedItems = orderedItems;
        }

        public SkillCode GetSkillCode()
        {
            _orderedItems.First().TryGetSlipDamageCode(out var slipCode);
            MyDebug.Assert(slipCode != SlipDamageCode.NoSlipDamage);
            var skillCode = slipCode switch
            {
                SlipDamageCode.Poisoning => SkillCode.Poisoning,
                _ => throw new ArgumentOutOfRangeException()
            };

            return skillCode;
        }
    }
}