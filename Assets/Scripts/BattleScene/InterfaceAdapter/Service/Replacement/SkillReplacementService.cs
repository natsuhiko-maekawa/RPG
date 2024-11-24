using System;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using Utility;

namespace BattleScene.InterfaceAdapter.Service.Replacement
{
    public class SkillReplacementService : IReplacementService
    {
        public string Replacement { get; }= "[skill]";
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