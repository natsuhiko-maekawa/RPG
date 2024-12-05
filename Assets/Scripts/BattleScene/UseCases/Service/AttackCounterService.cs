using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class AttackCounterService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;

        public AttackCounterService(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository,
            IFactory<BattlePropertyValueObject> battlePropertyFactory)
        {
            _characterRepository = characterRepository;
            _battleLogRepository = battleLogRepository;
            _battlePropertyFactory = battlePropertyFactory;
        }

        public float GetRate()
        {
            var rate = (float)Count() / _battlePropertyFactory.Create().AttackCountLimit;
            return Math.Min(rate, 1.0f);
        }

        public bool IsOverflow()
        {
            return _battlePropertyFactory.Create().AttackCountLimit < Count();
        }

        private int Count()
        {
            var battleLogList = _battleLogRepository.Get();
            var player = _characterRepository.Get().Single(x => x.IsPlayer);
            var playerId = player.Id;
            var count = battleLogList
                .OrderByDescending(x => x)
                .TakeWhile(x => x.BattleEventCode != BattleEventCode.FatalitySkill)
                .Where(x => Equals(x.ActorId, playerId))
                .Select(x => x.AttackList
                    .Count(y => y.IsHit))
                .Sum();
            return count;
        }
    }
}