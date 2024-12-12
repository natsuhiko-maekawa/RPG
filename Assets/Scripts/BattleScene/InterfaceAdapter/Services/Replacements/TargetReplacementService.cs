using System;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.InterfaceAdapter.Services.Replacements.Interfaces;
using Cysharp.Text;
using Utility;

namespace BattleScene.InterfaceAdapter.Services.Replacements
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
            var targetList = _battleLog.GetLast().TargetList;
            MyDebug.Assert(targetList.Count > 0);
            if (targetList.Count == 0) return ReadOnlySpan<char>.Empty;
            using (var stringBuilder = ZString.CreateStringBuilder())
            {
                var firstTargetId = targetList.First();
                var targetName = _replacementCommon.GetCharacterName(firstTargetId);
                stringBuilder.Append(targetName);
                if (targetList.Count > 1) stringBuilder.Append(ReplacementCommonService.TotalSuffix);
                var span = stringBuilder.AsSpan();
                return span;
            }
        }
    }
}