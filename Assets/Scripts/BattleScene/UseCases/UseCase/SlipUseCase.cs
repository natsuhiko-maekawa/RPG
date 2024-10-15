using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.Service;
using Utility;

namespace BattleScene.UseCases.UseCase
{
    public class SlipUseCase
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly PlayerDomainService _player;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly SlipDamageGeneratorService _slipDamageGenerator;
        private readonly ICollection<SlipEntity, SlipDamageCode> _slipCollection;

        public SlipUseCase(
            BattleLoggerService battleLogger,
            PlayerDomainService player,
            OrderedItemsDomainService orderedItems,
            SlipDamageGeneratorService slipDamageGenerator,
            ICollection<SlipEntity, SlipDamageCode> slipCollection)
        {
            _battleLogger = battleLogger;
            _player = player;
            _orderedItems = orderedItems;
            _slipDamageGenerator = slipDamageGenerator;
            _slipCollection = slipCollection;
        }

        public void Commit()
        {
            var slipDamage = _slipDamageGenerator.Generate();
            _slipCollection.Get(slipDamage.SlipDamageCode).AdvanceTurn();
            _battleLogger.Log(slipDamage);
        }

        public SkillCode GetSkillCode()
        {
            _orderedItems.First().TryGetSlipDamageCode(out var slipCode);
            MyDebug.Assert(slipCode != SlipDamageCode.NoSlipDamage);
            var skillCode = slipCode switch
            {
                SlipDamageCode.Poisoning => SkillCode.Poisoning,
                SlipDamageCode.Suffocation => SkillCode.Suffocation,
                _ => throw new ArgumentOutOfRangeException()
            };

            return skillCode;
        }

        public IReadOnlyList<CharacterId> GetTargetList()
        {
            var targetList = new List<CharacterId>() { _player.GetId() };
            return targetList;
        }
    }
}