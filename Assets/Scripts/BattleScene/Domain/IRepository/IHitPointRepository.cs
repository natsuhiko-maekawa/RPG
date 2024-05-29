using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IRepository
{
    public interface IHitPointRepository
    {
        public ImmutableList<HitPointAggregate> Select();
        public HitPointAggregate Select(CharacterId characterId);
        public ImmutableList<HitPointAggregate> Select(IList<CharacterId> characterIdList);
        public void Update(HitPointAggregate hitPointAggregate);
    }
}