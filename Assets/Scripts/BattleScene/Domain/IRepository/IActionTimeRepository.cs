using System.Collections.Generic;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IRepository
{
    public interface IActionTimeRepository
    {
        public ActionTimeEntity Select(CharacterId characterId);
        public void Update(IList<ActionTimeEntity> actionTimeEntityList);
        public void Delete(CharacterId characterId);
    }
}