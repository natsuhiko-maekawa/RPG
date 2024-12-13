using System.Collections.Generic;
using BattleScene.Domain.Entities;
using BattleScene.UseCases.IServices;

namespace BattleScene.UseCases.UseCases
{
    public class CharacterDeadUseCase
    {
        private readonly IDeadCharacterService _deadCharacter;

        public CharacterDeadUseCase(
            IDeadCharacterService deadCharacter)
        {
            _deadCharacter = deadCharacter;
        }

        public bool IsPlayerDeadInThisTurn() => _deadCharacter.IsPlayerDeadInThisTurn();
        public bool IsAllEnemyDead() => _deadCharacter.IsAllEnemyDead();
        public IReadOnlyList<CharacterEntity> GetDeadCharacterInThisTurn()
            => _deadCharacter.GetDeadCharacterInThisTurn();
        public void ConfirmedDead() => _deadCharacter.ConfirmedDead();
    }
}