using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class HitPointRepository : IHitPointRepository
    {
        public ImmutableList<HitPointAggregate> Select()
        {
            throw new System.NotImplementedException();
        }

        public HitPointAggregate Select(CharacterId characterId)
        {
            throw new System.NotImplementedException();
        }

        public ImmutableList<HitPointAggregate> Select(IList<CharacterId> characterIdList)
        {
            throw new System.NotImplementedException();
        }

        public void Update(HitPointAggregate hitPointAggregate)
        {
            throw new System.NotImplementedException();
        }
    }
}