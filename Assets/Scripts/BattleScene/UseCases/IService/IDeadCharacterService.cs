using System.Collections.Generic;
using BattleScene.Domain.Entity;

namespace BattleScene.UseCases.IService
{
    public interface IDeadCharacterService
    {
        public bool IsAnyCharacterDeadInThisTurn();
        public bool IsPlayerDeadInThisTurn();
        public bool IsAllEnemyDead();
        public IReadOnlyList<CharacterEntity> GetDeadCharacterInThisTurn();
        public void ConfirmedDead();
    }
}