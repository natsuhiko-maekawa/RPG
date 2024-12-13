using System;
using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.SkillComponents.BaseClass;
using BattleScene.Presenters.Skills.BaseClass;
using BattleScene.UseCases.IServices;
using Range = BattleScene.Domain.Codes.Range;

namespace BattleScene.Presenters.Skills
{
    /// <summary>
    ///     命を刈る鎌
    /// </summary>
    public class PutScytheSkill : BaseSkill
    {
        private readonly IMyRandomService _myRandom;
        private readonly long _seed;

        public PutScytheSkill(
            IMyRandomService myRandom)
        {
            _myRandom = myRandom;
            _seed = DateTime.Now.Ticks;
        }

        public override SkillCode SkillCode { get; } = SkillCode.PutScythe;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode => GetAttackMessageCode();

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new BasicDamage() };

        private MessageCode GetAttackMessageCode()
        {
            return _myRandom.Choice(
                new[] { MessageCode.CutArmMessage, MessageCode.CutLegMessage, MessageCode.CutStomachMessage }, _seed);
        }
    }
}