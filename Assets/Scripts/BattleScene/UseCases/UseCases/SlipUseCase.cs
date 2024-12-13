using System;
using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.DomainServices;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;
using BattleScene.UseCases.Services;

namespace BattleScene.UseCases.UseCases
{
    public class SlipUseCase
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly IDeadCharacterService _deadCharacter;
        private readonly IHitPointService _hitPoint;
        private readonly PlayerDomainService _player;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly SlipDamageRegistererService _slipDamageRegisterer;
        private readonly IRepository<SlipEntity, SlipCode> _slipRepository;

        public SlipUseCase(
            BattleLoggerService battleLogger,
            IDeadCharacterService deadCharacter,
            IHitPointService hitPoint,
            PlayerDomainService player,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            SlipDamageRegistererService slipDamageRegisterer,
            IRepository<SlipEntity, SlipCode> slipRepository)
        {
            _battleLogger = battleLogger;
            _deadCharacter = deadCharacter;
            _hitPoint = hitPoint;
            _player = player;
            _skillFactory = skillFactory;
            _slipDamageRegisterer = slipDamageRegisterer;
            _slipRepository = slipRepository;
        }

        public void RegisterBattleEvent(SlipCode slipCode)
        {
            _slipDamageRegisterer.Register(slipCode);
            var slipDamageEvent = _battleLogger.GetLast();
            _hitPoint.Damaged(slipDamageEvent);
            _slipRepository.Get(slipDamageEvent.SlipCode).AdvanceTurn();
        }

        public SkillValueObject GetSkillCode(SlipCode slipCode)
        {
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

        public IReadOnlyList<CharacterEntity> GetTargetList()
        {
            var targetArray = new[] { _player.Get() };
            return targetArray;
        }

        public bool IsPlayerDeadInThisTurn() => _deadCharacter.IsPlayerDeadInThisTurn();
    }
}