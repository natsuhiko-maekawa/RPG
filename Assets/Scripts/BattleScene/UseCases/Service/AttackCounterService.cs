using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using UnityEngine;

namespace BattleScene.UseCases.Service
{
    public class AttackCounterService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;

        public AttackCounterService(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository)
        {
            _characterRepository = characterRepository;
            _battleLogRepository = battleLogRepository;
        }

        public float GetRate()
        {
            return Mathf.Min((float)Count() / Domain.Constant.AttackCountUpperLimit, 1.0f);
        }

        public bool IsOverflow()
        {
            return Domain.Constant.AttackCountUpperLimit < Count();
        }

        private int Count()
        {
            var battleLogList = _battleLogRepository.Select();
            var player = _characterRepository.Select().First(x => x.IsPlayer);
            var playerId = player.Id;
            return battleLogList
                .OrderByDescending(x => x)
                .TakeWhile(x => x.ActionCode == ActionCode.FatalitySkill)
                .Where(x => Equals(x.ActorId, playerId))
                .Select(x => x.AttackList
                    .Count(y => y.IsHit))
                .Sum();
        }
    }
}