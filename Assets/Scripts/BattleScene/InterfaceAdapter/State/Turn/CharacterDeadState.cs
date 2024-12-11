using BattleScene.InterfaceAdapter.PresenterFacade;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class CharacterDeadState : BaseState
    {
        private readonly CharacterDeadUseCase _useCase;
        private readonly CharacterDeadPresenterFacade _facade;
        private readonly AdvanceTurnState _advanceTurnState;

        public CharacterDeadState(
            CharacterDeadUseCase useCase,
            CharacterDeadPresenterFacade facade,
            AdvanceTurnState advanceTurnState)
        {
            _useCase = useCase;
            _facade = facade;
            _advanceTurnState = advanceTurnState;
        }

        public override void Start()
        {
            var deadCharacter = _useCase.GetDeadInThisTurn();
            _useCase.ConfirmedDead();
            _facade.Output(deadCharacter);
        }

        public override void Select()
        {
            Context.TransitionTo(_advanceTurnState);
        }
    }
}