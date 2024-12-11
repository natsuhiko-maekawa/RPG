using System.Collections.Generic;
using BattleScene.Domain.Entity;

namespace BattleScene.UseCases.IService
{
    public interface IDeadCharacterService
    {
        public bool DeadInThisTurn();
        public IReadOnlyList<CharacterEntity> GetDeadInThisTurn();
        public void ConfirmedDead();
    }
}