using System;
using BattleScene.Domain.DomainService;
using Cysharp.Text;

namespace BattleScene.InterfaceAdapter.Service.Replacement
{
    public class TechnicalPointReplacementService : IReplacementService
    {
        public string Replacement { get; }= "[technicalPoint]";
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