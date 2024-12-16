using BattleScene.Domain.Ids;

namespace BattleScene.UseCases.IServices
{
    public interface ISpeedService
    {
        public int GetSpeed(CharacterId characterId);
        public int GetAgility(CharacterId characterId);
    }
}