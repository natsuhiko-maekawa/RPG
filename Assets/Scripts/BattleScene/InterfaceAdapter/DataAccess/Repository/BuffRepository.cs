using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class BuffRepository : IBuffRepository
    {
        private readonly HashSet<BuffEntity> _buffSet = new();
        
        public ImmutableList<BuffEntity> Select(CharacterId characterId)
        {
            return _buffSet
                .Where(x => Equals(x.CharacterId, characterId))
                .ToImmutableList();
        }

        public BuffEntity Select(CharacterId characterId, BuffCode buffCode)
        {
            throw new System.NotImplementedException();
        }

        public void Update(BuffEntity buffEntity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(IList<BuffEntity> buffEntityList)
        {
            throw new System.NotImplementedException();
        }
    }
}