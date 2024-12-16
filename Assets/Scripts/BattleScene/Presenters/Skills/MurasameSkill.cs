using System;
using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.SkillComponents.BaseClass;
using BattleScene.Presenters.Skills.BaseClass;
using Range = BattleScene.Domain.Codes.Range;

namespace BattleScene.Presenters.Skills
{
    /// <summary>
    ///     村雨
    /// </summary>
    public class MurasameSkill : BaseSkill
    {
        private readonly IRepository<SlipEntity, SlipCode> _slipRepository;

        public MurasameSkill(
            IRepository<SlipEntity, SlipCode> slipRepository)
        {
            _slipRepository = slipRepository;
        }

        public override SkillCode SkillCode { get; } = SkillCode.Murasame;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.SkillMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new BasicDamage() };

        public override IReadOnlyList<BaseRecovery> RecoveryList => GetResetList();

        private IReadOnlyList<BaseRecovery> GetResetList()
        {
            return _slipRepository.Get(SlipCode.Burning).Effects
                ? new[] { new BurningRecovery() }
                : Array.Empty<BaseRecovery>();
        }
    }
}