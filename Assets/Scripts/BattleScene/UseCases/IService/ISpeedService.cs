using BattleScene.Domain.Ids;

namespace BattleScene.UseCases.IService
{
    public interface ISpeedService
    {
        public int GetSpeed(CharacterId characterId);
        public int GetAgility(CharacterId characterId);
    }
}