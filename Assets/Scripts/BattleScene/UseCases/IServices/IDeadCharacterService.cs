using System.Collections.Generic;
using BattleScene.Domain.Entities;
using BattleScene.UseCases.Services;

namespace BattleScene.UseCases.IServices
{
    public interface IDeadCharacterService
    {
        public bool IsAnyCharacterDeadInThisTurn();
        public bool IsPlayerDeadInThisTurn();
        public IReadOnlyList<CharacterEntity> GetDeadCharacterInThisTurn();
        public Dead GetDeadInThisTurn();
        public void ConfirmedDead();
        public bool IsAllEnemyDead();
    }
}