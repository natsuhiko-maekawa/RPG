using System.Collections.Generic;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using Utility;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class SelectorRepository : ISelectorRepository
    {
        private readonly HashSet<SelectorAggregate> _selectorSet = new();
        
        public SelectorAggregate Select(SelectorId id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(SelectorAggregate selector)
        {
            _selectorSet.Update(selector);
        }
    }
}