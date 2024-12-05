using System.Collections.Generic;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.IService
{
    public interface ICharacterCreatorService
    {
        // TODO: フラグ引数を使用している
        public void Create(CharacterId characterId, bool isPlayer = false);
        public void Create(IEnumerable<CharacterId> characterIdList);
    }
}