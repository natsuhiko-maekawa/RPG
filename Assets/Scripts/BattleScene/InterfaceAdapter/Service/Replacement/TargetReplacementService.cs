using System;
using System.Linq;
using BattleScene.Domain.DomainService;
using Cysharp.Text;
using Utility;

namespace BattleScene.InterfaceAdapter.Service.Replacement
{
    public class TargetReplacementService : IReplacementService
    {
        public string Replacement { get; }= "[target]";
        private readonly BattleLogDomainService _battleLog;
        private readonly ReplacementCommonService _replacementCommon;


        public TargetReplacementService(
            BattleLogDomainService battleLog,
            ReplacementCommonService replacementCommon)
        {
            _battleLog = battleLog;
            _replacementCommon = replacementCommon;
        }

        public bool IsMatch(string value) => value == Replacement;
        public ReadOnlySpan<char> GetNewCharSpan()
        {
            var targetIdList = _battleLog.GetLast().TargetIdList;
            MyDebug.Assert(targetIdList.Count > 0);
            if (targetIdList.Count == 0) return ReadOnlySpan<char>.Empty;
            using (var stringBuilder = ZString.CreateStringBuilder())
            {
                var firstTargetId = targetIdList.First();
                var targetName = _replacementCommon.GetCharacterName(firstTargetId);
                stringBuilder.Append(targetName);
                if (targetIdList.Count > 1) stringBuilder.Append(ReplacementCommonService.TotalSuffix);
                var span = stringBuilder.AsSpan();
                return span;
            }
        }
    }
}