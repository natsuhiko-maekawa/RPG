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
        private readonly ICollection<BattleLogEntity, BattleLogId> _battleLogCollection;
        private readonly ICollection<TurnEntity, TurnId> _turnCollection;

        public BattleLoggerService(
            ICollection<BattleLogEntity, BattleLogId> battleLogCollection,
            ICollection<TurnEntity, TurnId> turnCollection)
        {
            _battleLogCollection = battleLogCollection;
            _turnCollection = turnCollection;
        }

        public void Log(PrimeSkillValueObject primeSkill)
        {
            var (battleLogId, sequence, turn) = GetBattleLogCommonArguments();
            var battleLog = new BattleLogEntity(
                battleLogId: battleLogId,
                sequence: sequence,
                turn: turn,
                primeSkill: primeSkill);
            _battleLogCollection.Add(battleLog);
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
            _battleLogCollection.Add(battleLogList);
        }

        private (BattleLogId battleLogId, int nextSequence, int turn) GetBattleLogCommonArguments()
        {
            var battleLogId = new BattleLogId();
            var sequence = _battleLogCollection.Get()
                .Max()
                ?.Sequence ?? 0;
            var nextSequence = sequence + 1;
            var turn = _turnCollection.Get()
                .First()
                .Turn;
            return (battleLogId, nextSequence, turn);
        }
    }
}