using System.Collections.Generic;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.IService
{
    public interface ICharacterCreatorService
    {
        public void Create(CharacterId characterId);
        public void Create(IEnumerable<CharacterId> characterIdList);
    }
}