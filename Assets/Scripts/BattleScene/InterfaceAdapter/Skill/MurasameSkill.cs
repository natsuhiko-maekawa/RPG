using System.Collections.Generic;
using Utility;
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
        private readonly ICollection<SlipEntity, SlipCode> _slipCollection;

        public MurasameSkill(
            ICollection<SlipEntity, SlipCode> slipCollection)
        {
            _slipCollection = slipCollection;
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
            return _slipCollection.Get(SlipCode.Burning).Effects
                ? new[] { new BurningRecovery() }
                : MyList<BaseRecovery>.Empty;
        }
    }
}