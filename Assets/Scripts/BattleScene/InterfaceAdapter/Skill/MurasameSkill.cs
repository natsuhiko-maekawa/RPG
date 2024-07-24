using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Skill.SkillElement;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     村雨
    /// </summary>
    internal class MurasameSkill : AbstractSkill
    {
        private readonly IRepository<SlipDamageEntity, SlipDamageId> _slipDamageRepository;

        public MurasameSkill(
            IRepository<SlipDamageEntity, SlipDamageId> slipDamageRepository)
        {
            _slipDamageRepository = slipDamageRepository;
        }

        public override SkillCode SkillCode { get; } = SkillCode.Murasame;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Solo;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Katana;
        public override MessageCode Description { get; } = MessageCode.MurasameDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AttackMessage;

        public override ImmutableList<AbstractDamage> DamageList { get; }
            = ImmutableList.Create<AbstractDamage>(new BasicDamage());

        public override ImmutableList<AbstractReset> ResetList => GetResetList();

        private ImmutableList<AbstractReset> GetResetList()
        {
            var slipDamageId = new SlipDamageId(SlipDamageCode.Burning);
            return _slipDamageRepository.Select(slipDamageId) == null
                ? ImmutableList<AbstractReset>.Empty
                : ImmutableList.Create<AbstractReset>(new BurningReset());
        }
    }
}