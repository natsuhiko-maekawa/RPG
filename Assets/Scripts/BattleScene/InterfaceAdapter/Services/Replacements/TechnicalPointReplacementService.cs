using System;
using BattleScene.Domain.DomainServices;
using BattleScene.InterfaceAdapter.Services.Replacements.Interfaces;
using Cysharp.Text;

namespace BattleScene.InterfaceAdapter.Services.Replacements
{
    public class TechnicalPointReplacementService : IReplacementService
    {
        public string Replacement => "[technicalPoint]";
        private readonly BattleLogDomainService _battleLog;

        public TechnicalPointReplacementService(
            BattleLogDomainService battleLog)
        {
            _battleLog = battleLog;
        }

        public bool IsMatch(string value) => value == Replacement;
        public ReadOnlySpan<char> GetNewCharSpan()
        {
            var technicalPoint = _battleLog.GetLast().TechnicalPoint;
            using (var stringBuilder = ZString.CreateStringBuilder())
            {
                stringBuilder.Append(technicalPoint);
                var span = stringBuilder.AsSpan();
                return span;
            }
        }
    }
}