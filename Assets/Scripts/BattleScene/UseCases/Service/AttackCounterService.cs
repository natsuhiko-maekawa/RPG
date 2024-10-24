using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.Service
{
    public class AttackCounterService
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly ICollection<BattleLogEntity, BattleLogId> _battleLogCollection;

        public AttackCounterService(
            ICollection<CharacterEntity, CharacterId> characterCollection,
            ICollection<BattleLogEntity, BattleLogId> battleLogCollection)
        {
            _characterCollection = characterCollection;
            _battleLogCollection = battleLogCollection;
        }

        public float GetRate()
        {
            return Math.Min((float)Count() / Constant.AttackCountUpperLimit, 1.0f);
        }

        public bool IsOverflow()
        {
            return Constant.AttackCountUpperLimit < Count();
        }

        private int Count()
        {
            var battleLogList = _battleLogCollection.Get();
            var player = _characterCollection.Get().Single(x => x.IsPlayer);
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