using System;
using System.Linq;
using BattleScene.Domain.DomainServices;
using BattleScene.InterfaceAdapter.Services.Replacements.Interfaces;
using Cysharp.Text;

namespace BattleScene.InterfaceAdapter.Services.Replacements
{
    public class DamageReplacementService : IReplacementService
    {
        public string Replacement => "[damage]";
        private readonly BattleLogDomainService _battleLog;

        public DamageReplacementService(
            BattleLogDomainService battleLog)
        {
            _battleLog = battleLog;
        }

        public bool IsMatch(string value) => value == Replacement;
        public ReadOnlySpan<char> GetNewCharSpan()
        {
            var attackList = _battleLog.GetLast().AttackList;
            using (var stringBuilder = ZString.CreateStringBuilder())
            {
                if (attackList.Count(x => x.IsHit) > 1) stringBuilder.Append(ReplacementCommonService.TotalPrefix);
                var damage = attackList
                    .Where(x => x.IsHit)
                    .Select(x => x.Amount)
                    .Sum();
                stringBuilder.Append(damage);
                var span = stringBuilder.AsSpan();
                return span;
            }
        }
    }
}