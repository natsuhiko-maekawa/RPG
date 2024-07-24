using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using Utility;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly HashSet<SkillEntity> _skillSet = new();
        
        public SkillEntity Select(CharacterId characterId)
        {
            return _skillSet
                .First(x => Equals(x.Id, characterId));
        }

        public void Update(SkillEntity skillEntity)
        {
            _skillSet.Update(skillEntity);
        }
    }
}