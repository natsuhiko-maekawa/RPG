using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using Utility;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class ActionTimeRepository : IActionTimeRepository
    {
        private readonly HashSet<ActionTimeEntity> _actionTimeSet = new();

        public ActionTimeEntity Select(CharacterId characterId)
        {
            return _actionTimeSet
                .First(x => Equals(x.CharacterId, characterId));
        }

        public void Update(IList<ActionTimeEntity> actionTimeList)
        {
            foreach (var actionTime in actionTimeList)
                _actionTimeSet.Update(actionTime);
        }

        public void Delete(CharacterId characterId)
        {
            _actionTimeSet.RemoveWhere(x => Equals(x.CharacterId, characterId));
        }
    }
}