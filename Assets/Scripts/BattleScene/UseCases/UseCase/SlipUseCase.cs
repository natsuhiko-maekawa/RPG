﻿using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
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
        private readonly ICollection<SlipEntity, SlipCode> _slipCollection;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;

        public SlipUseCase(
            BattleLoggerService battleLogger,
            PlayerDomainService player,
            OrderedItemsDomainService orderedItems,
            SlipDamageGeneratorService slipDamageGenerator,
            ICollection<SlipEntity, SlipCode> slipCollection,
            IFactory<SkillValueObject, SkillCode> skillFactory)
        {
            _battleLogger = battleLogger;
            _player = player;
            _orderedItems = orderedItems;
            _slipDamageGenerator = slipDamageGenerator;
            _slipCollection = slipCollection;
            _skillFactory = skillFactory;
        }

        public void Commit()
        {
            var slipDamage = _slipDamageGenerator.Generate();
            _slipCollection.Get(slipDamage.SlipCode).AdvanceTurn();
            _battleLogger.Log(slipDamage);
        }

        public SkillValueObject GetSkillCode()
        {
            _orderedItems.First().TryGetSlipDamageCode(out var slipCode);
            MyDebug.Assert(slipCode != SlipCode.NoSlip);
            var skillCode = slipCode switch
            {
                SlipCode.Burning => SkillCode.Burning,
                SlipCode.Poisoning => SkillCode.Poisoning,
                SlipCode.Bleeding => SkillCode.Bleeding,
                SlipCode.Suffocation => SkillCode.Suffocation,
                _ => throw new ArgumentOutOfRangeException()
            };

            var skill = _skillFactory.Create(skillCode);
            return skill;
        }

        public IReadOnlyList<CharacterId> GetTargetList()
        {
            var targetList = new List<CharacterId>() { _player.GetId() };
            return targetList;
        }
    }
}