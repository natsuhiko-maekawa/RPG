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
    internal class MurasameSkill : BaseSkill
    {
        private readonly ICollection<SlipEntity, SlipDamageCode> _slipDamageCollection;

        public MurasameSkill(
            ICollection<SlipEntity, SlipDamageCode> slipDamageCollection)
        {
            _slipDamageCollection = slipDamageCollection;
        }

        public override SkillCode SkillCode { get; } = SkillCode.Murasame;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AttackMessage;

        public override ImmutableList<BaseDamage> DamageList { get; }
            = ImmutableList.Create<BaseDamage>(new BasicDamage());

        public override ImmutableList<BaseReset> ResetList => GetResetList();

        private ImmutableList<BaseReset> GetResetList()
        {
            return _slipDamageCollection.Get(SlipDamageCode.Burning) == null
                ? ImmutableList<BaseReset>.Empty
                : ImmutableList.Create<BaseReset>(new BurningReset());
        }
    }
}