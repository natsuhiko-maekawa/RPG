using System.Collections.Generic;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using Utility;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class SkillSelectorRepository : ISkillSelectorRepository
    {
        private readonly HashSet<SkillSelectorAggregate> _skillSelectorSet = new();
        
        public SkillSelectorAggregate Select(SkillSelectorId id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(SkillSelectorAggregate skillSelector)
        {
            _skillSelectorSet.Update(skillSelector);
        }
    }
}