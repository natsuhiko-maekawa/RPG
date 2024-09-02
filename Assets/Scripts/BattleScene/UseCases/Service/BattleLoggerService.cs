using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
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

        public void Log(BuffValueObject buff)
        {
            var battleLogId = new BattleLogId();
            var sequence = _battleLogRepository.Select()
                .Max()
                ?.Sequence ?? 0;
            var nextSequence = sequence + 1;
            var turn = _turnRepository.Select()
                .First()
                .Turn;
            var battleLog = new BattleLogEntity(
                battleLogId: battleLogId,
                sequence: nextSequence,
                turn: turn,
                buff: buff);
            _battleLogRepository.Update(battleLog);
        }

        public void Log(DamageValueObject damage)
        {
            var battleLogId = new BattleLogId();
            var sequence = _battleLogRepository.Select()
                .Max()
                ?.Sequence ?? 0;
            var nextSequence = sequence + 1;
            var turn = _turnRepository.Select()
                .First()
                .Turn;
            var battleLog = new BattleLogEntity(
                battleLogId: battleLogId,
                sequence: nextSequence,
                turn: turn,
                damage: damage);
            _battleLogRepository.Update(battleLog);
        }
    }
}