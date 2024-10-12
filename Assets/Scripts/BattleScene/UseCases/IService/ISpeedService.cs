using BattleScene.Domain.Id;

namespace BattleScene.UseCases.IService
{
    public interface ISpeedService
    {
        public int Get(CharacterId characterId);
    }
}