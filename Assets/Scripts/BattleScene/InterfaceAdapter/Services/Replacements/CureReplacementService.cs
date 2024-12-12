using System;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.InterfaceAdapter.Services.Replacements.Interfaces;
using Cysharp.Text;

namespace BattleScene.InterfaceAdapter.Services.Replacements
{
    public class CureReplacementService : IReplacementService
    {
        public string Replacement => "[cure]";
        private readonly BattleLogDomainService _battleLog;

        public CureReplacementService(
            BattleLogDomainService battleLog)
        {
            _battleLog = battleLog;
        }

        public bool IsMatch(string value) => value == Replacement;
        public ReadOnlySpan<char> GetNewCharSpan()
        {
            var curingList = _battleLog.GetLast().CuringList;
            using (var stringBuilder = ZString.CreateStringBuilder())
            {
                if (curingList.Count > 1) stringBuilder.Append(ReplacementCommonService.TotalPrefix);
                var cure = curingList
                    .Select(x => x.Amount)
                    .Sum();
                stringBuilder.Append(cure);
                var span = stringBuilder.AsSpan();
                return span;
            }
        }
    }
}