using System;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DomainServices;
using BattleScene.InterfaceAdapter.Services.Replacements.Interfaces;
using Utility;

namespace BattleScene.InterfaceAdapter.Services.Replacements
{
    public class SkillReplacementService : IReplacementService
    {
        public string Replacement => "[skill]";
        private readonly BattleLogDomainService _battleLog;
        private readonly IResource<SkillViewDto, SkillCode> _skillViewInfoResource;

        public SkillReplacementService(
            BattleLogDomainService battleLog,
            IResource<SkillViewDto, SkillCode> skillViewInfoResource)
        {
            _battleLog = battleLog;
            _skillViewInfoResource = skillViewInfoResource;
        }

        public bool IsMatch(string value) => value == Replacement;
        public ReadOnlySpan<char> GetNewCharSpan()
        {
            var skillCode = _battleLog.GetLast().SkillCode;
            MyDebug.Assert(skillCode != SkillCode.NoSkill);
            if (skillCode == SkillCode.NoSkill) return string.Empty;
            var skillName = _skillViewInfoResource.Get(skillCode).SkillName;
            return skillName;
        }
    }
}