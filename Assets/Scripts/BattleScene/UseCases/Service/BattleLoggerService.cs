using System.Collections.Generic;
using System.Linq;
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

        public void Log(PrimeSkillValueObject primeSkill)
        {
            var (battleLogId, sequence, turn) = GetBattleLogCommonArguments();
            var battleLog = new BattleLogEntity(
                battleLogId: battleLogId,
                sequence: sequence,
                turn: turn,
                primeSkill: primeSkill);
            _battleLogRepository.Update(battleLog);
        }

        public void Log(IReadOnlyList<PrimeSkillValueObject> primeSkillList)
        {
            var (battleLogId, sequence, turn) = GetBattleLogCommonArguments();
            var battleLogList = primeSkillList
                .Select(x => new BattleLogEntity(
                    battleLogId: battleLogId,
                    sequence: sequence,
                    turn: turn,
                    primeSkill: x))
                .ToList();
            _battleLogRepository.Update(battleLogList);
        }

        private (BattleLogId battleLogId, int nextSequence, int turn) GetBattleLogCommonArguments()
        {
            var battleLogId = new BattleLogId();
            var sequence = _battleLogRepository.Select()
                .Max()
                ?.Sequence ?? 0;
            var nextSequence = sequence + 1;
            var turn = _turnRepository.Select()
                .First()
                .Turn;
            return (battleLogId, nextSequence, turn);
        }
    }
}