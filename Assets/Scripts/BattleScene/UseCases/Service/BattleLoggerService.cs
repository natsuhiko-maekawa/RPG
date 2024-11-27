using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class BattleLoggerService
    {
        private readonly ICollection<BattleLogEntity, BattleLogId> _battleLogCollection;
        private readonly ICollection<TurnEntity, TurnId> _turnCollection;

        public BattleLoggerService(
            ICollection<BattleLogEntity, BattleLogId> battleLogCollection,
            ICollection<TurnEntity, TurnId> turnCollection)
        {
            _battleLogCollection = battleLogCollection;
            _turnCollection = turnCollection;
        }

        public void Log((CharacterId? actorId, AilmentCode ailmentCode, SlipCode slipCode) tuple)
        {
            var (battleLogId, sequence, turn) = GetBattleLogCommonArguments();
            var (actorId, ailmentCode, slipCode) = tuple;
            var battleLog = new BattleLogEntity(
                battleLogId: battleLogId, 
                sequence: sequence, 
                turn: turn, 
                actorId: actorId, 
                ailmentCode: ailmentCode, 
                slipCode: slipCode);
            _battleLogCollection.Add(battleLog);
        }

        public void Log(SkillCode skillCode)
        {
            var battleLog = GetLastEntity();
            battleLog.Update(skillCode);
        }

        public void Log(BattleEventValueObject battleEvent)
        {
            var battleLog = GetLastEntity();
            battleLog.Update(battleEvent);
        }

        public void Log(IReadOnlyList<BattleEventValueObject> battleEventList)
        {
            for (var i = 0; i < battleEventList.Count; ++i)
            {
                Log(battleEventList[i]);
            }
        }

        private (BattleLogId battleLogId, int nextSequence, int turn) GetBattleLogCommonArguments()
        {
            var nextSequence = _battleLogCollection.Get()
                .Max()?.Sequence + 1 ?? 0;
            var turn = _turnCollection.TryGet(out var turnList)
                ? turnList
                    .Single().Turn
                : 0;
            var battleLogId = FindOrCreateIdBySequence(nextSequence);
            return (battleLogId, nextSequence, turn);
        }

        private BattleLogEntity GetLastEntity()
        {
            var sequence = _battleLogCollection.Get()
                .Max().Sequence;
            var battleLogId = FindOrCreateIdBySequence(sequence);
            var battleLog = _battleLogCollection.Get(battleLogId);
            return battleLog;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BattleLogId FindOrCreateIdBySequence(int sequence)
        {
            BattleLogId battleLogId;
            var battleCollection = _battleLogCollection.Get();
            for (var i = 0; i < battleCollection.Count; ++i)
            {
                if (sequence == battleCollection[i].Sequence)
                {
                    battleLogId = battleCollection[i].Id;
                    return battleLogId;
                }
            }

            battleLogId = new BattleLogId();
            return battleLogId;
        }
    }
}