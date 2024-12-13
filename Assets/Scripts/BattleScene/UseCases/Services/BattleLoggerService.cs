using System.Linq;
using System.Runtime.CompilerServices;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;

namespace BattleScene.UseCases.Services
{
    public class BattleLoggerService
    {
        private readonly IRepository<BattleEventEntity, BattleEventId> _battleLogRepository;
        private readonly IRepository<TurnEntity, TurnId> _turnRepository;

        public BattleLoggerService(
            IRepository<BattleEventEntity, BattleEventId> battleLogRepository,
            IRepository<TurnEntity, TurnId> turnRepository)
        {
            _battleLogRepository = battleLogRepository;
            _turnRepository = turnRepository;
        }

        public void Log((CharacterEntity? actor, AilmentCode ailmentCode, SlipCode slipCode) tuple)
        {
            var (battleLogId, sequence, turn) = GetBattleLogCommonArguments();
            var (actorId, ailmentCode, slipCode) = tuple;
            var battleLog = new BattleEventEntity(
                battleEventId: battleLogId,
                sequence: sequence,
                turn: turn,
                actor: actorId,
                ailmentCode: ailmentCode, 
                slipCode: slipCode);
            _battleLogRepository.Add(battleLog);
        }

        public void Log(BattleEventEntity battleEvent)
        {
            _battleLogRepository.Add(battleEvent);
        }

        public BattleEventEntity GetLast()
        {
            return _battleLogRepository.Get().Max();
        }

        public bool IsSingleAsTurn(BattleEventEntity battleEvent)
        {
            return _battleLogRepository.Get()
                .Count(x => x.Sequence == battleEvent.Sequence) == 1;
        }

        private (BattleEventId battleLogId, int nextSequence, int turn) GetBattleLogCommonArguments()
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BattleEventId FindOrCreateIdBySequence(int sequence)
        {
            BattleEventId battleEventId;
            var battleCollection = _battleLogRepository.Get();
            for (var i = 0; i < battleCollection.Count; ++i)
            {
                if (sequence == battleCollection[i].Sequence)
                {
                    battleEventId = battleCollection[i].Id;
                    return battleEventId;
                }
            }

            battleEventId = new BattleEventId();
            return battleEventId;
        }
    }
}