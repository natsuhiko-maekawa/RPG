using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IRepository
{
    public interface ITargetRepository
    {
        public TargetEntity Select(CharacterId characterId);
        public void Update(TargetEntity targetEntity);
    }
}