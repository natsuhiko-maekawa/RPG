using BattleScene.UseCases.IServices;
using BattleScene.UseCases.Services;

namespace BattleScene.UseCases.UseCases
{
    public class DamageOutputUseCase
    {
        private readonly IDeadCharacterService _deadCharacter;

        public DamageOutputUseCase(IDeadCharacterService deadCharacter)
        {
            _deadCharacter = deadCharacter;
        }

        public Dead GetDeadInThisTurn() => _deadCharacter.GetDeadInThisTurn();
        public bool IsAnyCharacterDeadInThisTurn() => _deadCharacter.IsAnyCharacterDeadInThisTurn();
    }
}