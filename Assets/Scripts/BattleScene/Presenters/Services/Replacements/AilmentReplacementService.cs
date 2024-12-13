using System;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DomainServices;
using BattleScene.Presenters.Services.Replacements.Interfaces;
using Utility;

namespace BattleScene.Presenters.Services.Replacements
{
    public class AilmentReplacementService : IReplacementService
    {
        public string Replacement => "[ailment]";
        private readonly IResource<AilmentViewDto, AilmentCode, SlipCode> _ailmentViewResource;
        private readonly BattleLogDomainService _battleLog;

        public AilmentReplacementService(
            BattleLogDomainService battleLog,
            IResource<AilmentViewDto, AilmentCode, SlipCode> ailmentViewResource)
        {
            _battleLog = battleLog;
            _ailmentViewResource = ailmentViewResource;
        }

        public bool IsMatch(string value) => value == Replacement;
        public ReadOnlySpan<char> GetNewCharSpan()
        {
            var ailmentCode = _battleLog.GetLast().AilmentCode;
            MyDebug.Assert(ailmentCode != AilmentCode.NoAilment);
            if (ailmentCode == AilmentCode.NoAilment) return string.Empty;
            var ailmentName = _ailmentViewResource.Get(ailmentCode).Name;
            return ailmentName;
        }
    }
}