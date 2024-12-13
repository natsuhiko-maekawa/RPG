using BattleScene.Domain.Ids;

namespace BattleScene.UseCases.IService
{
    public interface ICharacterCreatorService
    {
        // TODO: フラグ引数を使用しているため、修正すること。
        public void Create(CharacterId characterId, bool isPlayer = false);
        public void Create(CharacterId[] characterIdList);
    }
}