using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using Utility;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class TargetRepository : ITargetRepository
    {
        private readonly HashSet<TargetEntity> _targetSet = new();
        
        public TargetEntity Select(CharacterId characterId)
        {
            return _targetSet
                .First(x => Equals(x.CharacterId, characterId));
        }

        public void Update(TargetEntity targetEntity)
        {
            _targetSet.Update(targetEntity);
        }
    }
}