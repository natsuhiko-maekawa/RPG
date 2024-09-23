using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
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
        private readonly IRepository<SlipDamageEntity, SlipDamageCode> _slipDamageRepository;

        public MurasameSkill(
            IRepository<SlipDamageEntity, SlipDamageCode> slipDamageRepository)
        {
            _slipDamageRepository = slipDamageRepository;
        }

        public override SkillCode SkillCode { get; } = SkillCode.Murasame;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Solo;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Katana;
        public override MessageCode Description { get; } = MessageCode.MurasameDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AttackMessage;

        public override ImmutableList<BaseDamage> DamageList { get; }
            = ImmutableList.Create<BaseDamage>(new BasicDamage());

        public override ImmutableList<BaseReset> ResetList => GetResetList();

        private ImmutableList<BaseReset> GetResetList()
        {
            return _slipDamageRepository.Select(SlipDamageCode.Burning) == null
                ? ImmutableList<BaseReset>.Empty
                : ImmutableList.Create<BaseReset>(new BurningReset());
        }
    }
}