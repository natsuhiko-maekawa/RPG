using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.UseCase
{
    public class SlipUseCase
    {
        private readonly BattleLoggerService _battleLogger;
        private readonly PlayerDomainService _player;
        private readonly SlipDamageRegistererService _slipDamageRegisterer;
        private readonly IRepository<SlipEntity, SlipCode> _slipRepository;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly IHitPointService _hitPoint;

        public SlipUseCase(
            BattleLoggerService battleLogger,
            PlayerDomainService player,
            SlipDamageRegistererService slipDamageRegisterer,
            IRepository<SlipEntity, SlipCode> slipRepository,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            IHitPointService hitPoint)
        {
            _battleLogger = battleLogger;
            _player = player;
            _slipDamageRegisterer = slipDamageRegisterer;
            _slipRepository = slipRepository;
            _skillFactory = skillFactory;
            _hitPoint = hitPoint;
        }

        public void RegisterBattleEvent(SlipCode slipCode)
        {
            _slipDamageRegisterer.Register(slipCode);
            var slipDamageEvent = _battleLogger.GetLast();
            _hitPoint.Damaged(slipDamageEvent);
            _slipRepository.Get(slipDamageEvent.SlipCode).AdvanceTurn();
            // _battleLogger.Log(slipDamage);
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
    }
}