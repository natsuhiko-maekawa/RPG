using System.Collections.Generic;
using BattleScene.Domain.Id;

namespace BattleScene.UseCases.IService
{
    public interface IDeadCharacterService
    {
        public bool DeadInThisTurn();
        public IReadOnlyList<CharacterId> GetDeadCharacterIdInThisTurn();
        public void DeleteDeadCharacter();
    }
}