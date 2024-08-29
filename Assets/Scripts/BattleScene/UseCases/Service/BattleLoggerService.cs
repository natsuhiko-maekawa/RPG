using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class BattleLoggerService
    {
        private readonly ISkillRepository _skillRepository;
        private readonly ITurnRepository _turnRepository;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;

        public void Log(DamageValueObject damage)
        {
            var battleLogId = new BattleLogId();
            var sequence = _battleLogRepository.Select()
                .OrderBy(x => x.Sequence)
                .Last()
                .Sequence;
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