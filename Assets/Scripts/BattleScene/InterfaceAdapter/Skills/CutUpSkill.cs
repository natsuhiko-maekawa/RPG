using System;
using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;
using BattleScene.InterfaceAdapter.Skills.BaseClass;
using BattleScene.UseCases.IService;
using static BattleScene.Domain.Codes.MessageCode;
using Range = BattleScene.Domain.Codes.Range;

namespace BattleScene.InterfaceAdapter.Skills
{
    /// <summary>
    ///     切り刻み
    /// </summary>
    public class CutUpSkill : BaseSkill
    {
        private readonly IMyRandomService _myRandom;
        private readonly long _seed;

        public CutUpSkill(
            IMyRandomService myRandom)
        {
            _myRandom = myRandom;
            _seed = DateTime.Now.Ticks;
        }

        public override SkillCode SkillCode { get; } = SkillCode.CutUp;
        public override Range Range { get; } = Range.Solo;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override MessageCode AttackMessageCode => GetAttackMessageCode();

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new FiveTimeDamage() };

        private MessageCode GetAttackMessageCode()
        {
            return _myRandom.Choice(new[] { CutUpArmMessage, CutUpLegMessage, CutUpStomachMessage }, _seed);
        }
    }
}