using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     村雨
    /// </summary>
    public class MurasameSkill : BaseSkill
    {
        private readonly ICollection<SlipEntity, SlipCode> _slipDamageCollection;

        public MurasameSkill(
            ICollection<SlipEntity, SlipCode> slipDamageCollection)
        {
            _slipDamageCollection = slipDamageCollection;
        }

        public override SkillCode SkillCode { get; } = SkillCode.Murasame;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AttackMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new BasicDamage() };

        public override IReadOnlyList<BaseReset> ResetList => GetResetList();

        private IReadOnlyList<BaseReset> GetResetList()
        {
            return _slipDamageCollection.Get(SlipCode.Burning) == null
                ? ImmutableList<BaseReset>.Empty
                : new[] { new BurningReset() };
        }
    }
}