using BattleScene.Domain.Entity;

namespace BattleScene.Domain.IRepository
{
    public interface ITurnRepository
    {
        public TurnEntity Select();
    }
}