using System;
using BattleScene.Domain.DomainService;
using Utility;

namespace BattleScene.InterfaceAdapter.Service.Replacement
{
    public class ActorReplacementService : IReplacementService
    {
        public string Replacement => "[actor]";
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
            var actor = _battleLog.GetLast().Actor;
            MyDebug.Assert(actor is not null);
            if (actor is null) return ReadOnlySpan<char>.Empty;
            var actorName = _replacementCommon.GetCharacterName(actor).AsSpan();
            return actorName;
        }
    }
}