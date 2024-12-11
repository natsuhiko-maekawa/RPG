using BattleScene.InterfaceAdapter.PresenterFacade;
using BattleScene.UseCases.UseCase;
using Utility;

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
            if (_useCase.IsPlayerDeadInThisTurn())
            {
                WhenPlayerDead();
            }
            else
            {
                WhenEnemyDead();
            }
        }

        private void WhenPlayerDead()
        {
            _facade.OutputWhenPlayerDead();
        }

        private void WhenEnemyDead()
        {
            var deadCharacter = _useCase.GetDeadCharacterInThisTurn();
            _useCase.ConfirmedDead();
            _facade.OutputWhenEnemyDead(deadCharacter);
        }

        public override void Select()
        {
            if (_useCase.IsPlayerDeadInThisTurn())
            {
                // 敗北ステートに遷移する
                return;
            }

            if (_useCase.IsAllEnemyDead())
            {
                // 勝利ステートに遷移する
                return;
            }

            Context.TransitionTo(_advanceTurnState);
        }
    }
}