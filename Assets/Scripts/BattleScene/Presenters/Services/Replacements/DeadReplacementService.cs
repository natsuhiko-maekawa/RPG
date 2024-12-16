using System;
using System.Linq;
using BattleScene.Domain.DomainServices;
using BattleScene.Presenters.Services.Replacements.Interfaces;
using Cysharp.Text;
using Utility;

namespace BattleScene.Presenters.Services.Replacements
{
    public class DeadReplacementService : IReplacementService
    {
        public string Replacement { get; } = "[dead]";
        private readonly BattleLogDomainService _battleLog;
        private readonly ReplacementCommonService _replacementCommon;

        public DeadReplacementService(
            BattleLogDomainService battleLog,
            ReplacementCommonService replacementCommon)
        {
            _battleLog = battleLog;
            _replacementCommon = replacementCommon;
        }

        public bool IsMatch(string value) => value == Replacement;

        public ReadOnlySpan<char> GetNewCharSpan()
        {
            var targetArray = _battleLog.GetLast().TargetList
                .Where(x => x.CurrentHitPoint == 0)
                .ToArray();
            MyDebug.Assert(targetArray.Length > 0);
            if (targetArray.Length == 0) return ReadOnlySpan<char>.Empty;
            using (var stringBuilder = ZString.CreateStringBuilder())
            {
                var firstTarget = targetArray.First();
                var targetName = _replacementCommon.GetCharacterName(firstTarget);
                stringBuilder.Append(targetName);
                if (targetArray.Length > 1) stringBuilder.Append(ReplacementCommonService.TotalSuffix);
                var span = stringBuilder.AsSpan();
                return span;
            }
        }
    }
}