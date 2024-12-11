using System.Collections.Generic;
using BattleScene.Domain.Entity;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.UseCase
{
    public class CharacterDeadUseCase
    {
        private readonly IDeadCharacterService _deadCharacter;

        public CharacterDeadUseCase(
            IDeadCharacterService deadCharacter)
        {
            _deadCharacter = deadCharacter;
        }

        public IReadOnlyList<CharacterEntity> GetDeadInThisTurn() => _deadCharacter.GetDeadInThisTurn();
        public void ConfirmedDead() => _deadCharacter.ConfirmedDead();
    }
}