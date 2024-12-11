using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.UseCase
{
    public class DamageOutputUseCase
    {
        private readonly IDeadCharacterService _deadCharacter;

        public DamageOutputUseCase(IDeadCharacterService deadCharacter)
        {
            _deadCharacter = deadCharacter;
        }

        public bool IsAnyCharacterDeadInThisTurn() => _deadCharacter.IsAnyCharacterDeadInThisTurn();
    }
}