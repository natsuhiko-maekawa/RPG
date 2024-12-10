using System;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using Utility;

namespace BattleScene.InterfaceAdapter.Service.Replacement
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