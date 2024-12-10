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
            _useCase.ConfirmedDead();
        }

        public override void Select()
        {
            Context.TransitionTo(_advanceTurnState);
        }
    }
}