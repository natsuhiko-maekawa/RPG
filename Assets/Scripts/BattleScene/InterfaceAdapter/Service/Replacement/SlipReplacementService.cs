using System;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using Utility;

namespace BattleScene.InterfaceAdapter.Service.Replacement
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