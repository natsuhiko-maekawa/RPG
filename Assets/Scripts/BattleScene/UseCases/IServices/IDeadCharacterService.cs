using System.Collections.Generic;
using BattleScene.Domain.Entities;

namespace BattleScene.UseCases.IServices
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