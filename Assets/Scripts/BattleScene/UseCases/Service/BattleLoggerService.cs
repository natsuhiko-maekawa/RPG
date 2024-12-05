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
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly IRepository<TurnEntity, TurnId> _turnRepository;

        public BattleLoggerService(
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            IRepository<TurnEntity, TurnId> turnRepository)
        {
            _battleLogRepository = battleLogRepository;
            _turnRepository = turnRepository;
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
            _battleLogRepository.Add(battleLog);
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
            var nextSequence = _battleLogRepository.Get()
                .Max()?.Sequence + 1 ?? 0;
            var turn = _turnRepository.TryGet(out var turnList)
                ? turnList
                    .Single().Turn
                : 0;
            var battleLogId = FindOrCreateIdBySequence(nextSequence);
            return (battleLogId, nextSequence, turn);
        }

        private BattleLogEntity GetLastEntity()
        {
            var sequence = _battleLogRepository.Get()
                .Max().Sequence;
            var battleLogId = FindOrCreateIdBySequence(sequence);
            var battleLog = _battleLogRepository.Get(battleLogId);
            return battleLog;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BattleLogId FindOrCreateIdBySequence(int sequence)
        {
            BattleLogId battleLogId;
            var battleCollection = _battleLogRepository.Get();
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