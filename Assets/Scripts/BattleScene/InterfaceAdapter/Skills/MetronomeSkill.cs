using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;
using BattleScene.InterfaceAdapter.Skills.BaseClass;
using BattleScene.UseCases.IService;
using Range = BattleScene.Domain.Codes.Range;

namespace BattleScene.InterfaceAdapter.Skills
{
    /// <summary>
    /// ゆびをふる
    /// </summary>
    public class MetronomeSkill : BaseSkill
    {
        private readonly IResource<CharacterPropertyDto, CharacterTypeCode> _characterPropertyResource;
        private readonly SkillServiceLocator _skillServiceLocator;
        private readonly IMyRandomService _myRandom;
        private readonly long _seed;

        public MetronomeSkill(
            IMyRandomService myRandom,
            SkillServiceLocator skillServiceLocator,
            IResource<CharacterPropertyDto, CharacterTypeCode> characterPropertyResource)
        {
            _myRandom = myRandom;
            _skillServiceLocator = skillServiceLocator;
            _characterPropertyResource = characterPropertyResource;
            _seed = DateTime.Now.Ticks;
        }

        public override SkillCode SkillCode { get; } = SkillCode.Metronome;
        public override int TechnicalPoint { get; } = 1;
        public override Range Range => GetSkill().Range;
        public override bool IsAutoTarget { get; } = true;
        public override MessageCode AttackMessageCode => GetSkill().AttackMessageCode;
        public override IReadOnlyList<BaseAilment> AilmentList => GetSkill().AilmentList;
        public override IReadOnlyList<BaseBuff> BuffList => GetSkill().BuffList;
        public override IReadOnlyList<BaseCure> CureList => GetSkill().CureList;
        public override IReadOnlyList<BaseDamage> DamageList => GetSkill().DamageList;
        public override IReadOnlyList<BaseDestroy> DestroyList => GetSkill().DestroyList;
        public override IReadOnlyList<BaseEnhance> EnhanceList => GetSkill().EnhanceList;
        public override IReadOnlyList<BaseRecovery> RecoveryList => GetSkill().RecoveryList;
        public override IReadOnlyList<BaseRestore> RestoreList => GetSkill().RestoreList;
        public override IReadOnlyList<BaseSlip> SlipList => GetSkill().SlipList;

        private BaseSkill GetSkill()
        {
            var playerSkillCodeList = _characterPropertyResource.Get(CharacterTypeCode.Player).SkillCodeList
                .Where(x => x != SkillCode.Metronome);
            var skillCode = _myRandom.Choice(playerSkillCodeList, _seed);
            var skill = _skillServiceLocator.Resolve(skillCode);
            return skill;
        }
    }
}