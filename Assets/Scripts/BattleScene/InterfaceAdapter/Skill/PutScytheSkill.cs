using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;
using BattleScene.UseCases.IService;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill
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