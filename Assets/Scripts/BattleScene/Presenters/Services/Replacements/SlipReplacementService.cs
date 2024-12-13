using System;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DomainServices;
using BattleScene.Presenters.Services.Replacements.Interfaces;
using Utility;

namespace BattleScene.Presenters.Services.Replacements
{
    public class SlipReplacementService : IReplacementService
    {
        public string Replacement => "[slip]";
        private readonly IResource<AilmentViewDto, AilmentCode, SlipCode> _ailmentViewResource;
        private readonly BattleLogDomainService _battleLog;

        public SlipReplacementService(
            BattleLogDomainService battleLog,
            IResource<AilmentViewDto, AilmentCode, SlipCode> ailmentViewResource)
        {
            _battleLog = battleLog;
            _ailmentViewResource = ailmentViewResource;
        }

        public bool IsMatch(string value) => value == Replacement;
        public ReadOnlySpan<char> GetNewCharSpan()
        {
            var slipCode = _battleLog.GetLast().SlipCode;
            MyDebug.Assert(slipCode != SlipCode.NoSlip);
            if (slipCode == SlipCode.NoSlip) return string.Empty;
            var slipName = _ailmentViewResource.Get(slipCode).Name;
            return slipName;
        }
    }
}