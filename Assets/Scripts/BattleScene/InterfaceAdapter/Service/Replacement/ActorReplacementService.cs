using System;
using BattleScene.Domain.DomainService;
using Utility;

namespace BattleScene.InterfaceAdapter.Service.Replacement
{
    public class ActorReplacementService : IReplacementService
    {
        public string Replacement { get; }= "[actor]";
        private readonly BattleLogDomainService _battleLog;
        private readonly ReplacementCommonService _replacementCommon;


        public ActorReplacementService(
            BattleLogDomainService battleLog,
            ReplacementCommonService replacementCommon)
        {
            _battleLog = battleLog;
            _replacementCommon = replacementCommon;
        }

        public bool IsMatch(string value) => value == Replacement;
        public ReadOnlySpan<char> GetNewCharSpan()
        {
            var actorId = _battleLog.GetLast().ActorId;
            MyDebug.Assert(actorId is not null);
            if (actorId is null) return ReadOnlySpan<char>.Empty;
            var actorName = _replacementCommon.GetCharacterName(actorId).AsSpan();
            return actorName;
        }
    }
}