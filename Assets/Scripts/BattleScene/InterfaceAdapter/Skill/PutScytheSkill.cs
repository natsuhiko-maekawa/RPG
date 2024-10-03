using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.UseCases.IService;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     命を刈る鎌
    /// </summary>
    internal class PutScytheSkill : BaseSkill
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
        public override ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList.Create(BodyPartCode.Arm);
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode => GetAttackMessageCode();

        public override ImmutableList<BaseDamage> DamageList { get; }
            = ImmutableList.Create<BaseDamage>(new BasicDamage());

        private MessageCode GetAttackMessageCode()
        {
            return _myRandom.Choice(
                new[] { MessageCode.CutArmMessage, MessageCode.CutLegMessage, MessageCode.CutStomachMessage }, _seed);
        }
    }
}