using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.UseCases.Service;
using Utility;

namespace BattleScene.UseCases.UseCase
{
    public class SlipUseCase
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly SlipDamageGeneratorService _slipDamageGenerator;

        public SlipUseCase(
            BattleLoggerService battleLogger,
            OrderedItemsDomainService orderedItems,
            SlipDamageGeneratorService slipDamageGenerator)
        {
            _battleLogger = battleLogger;
            _orderedItems = orderedItems;
            _slipDamageGenerator = slipDamageGenerator;
        }

        public void Commit()
        {
            var slipDamage = _slipDamageGenerator.Generate();
            _battleLogger.Log(slipDamage);
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