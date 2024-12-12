using BattleScene.InterfaceAdapter.PresenterFacade;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    // TODO: プレイヤーが死亡した場合と敵が死亡した場合でステートを分割するべきだった。
    public class CharacterDeadState : BaseState
    {
        private readonly CharacterDeadUseCase _useCase;
        private readonly CharacterDeadPresenterFacade _facade;
        private readonly AdvanceTurnState _advanceTurnState;
        private readonly TurnStopState _turnStopState;

        public CharacterDeadState(
            CharacterDeadUseCase useCase,
            CharacterDeadPresenterFacade facade,
            AdvanceTurnState advanceTurnState,
            TurnStopState turnStopState)
        {
            _useCase = useCase;
            _facade = facade;
            _advanceTurnState = advanceTurnState;
            _turnStopState = turnStopState;
        }

        public override void Start()
        {
            Context.NextStateCode = StateCode.Next;
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
            Context.NextStateCode = StateCode.PlayerLoseState;
        }

        private void WhenEnemyDead()
        {
            var deadCharacter = _useCase.GetDeadCharacterInThisTurn();
            _useCase.ConfirmedDead();
            _facade.OutputWhenEnemyDead(deadCharacter);
            if (_useCase.IsAllEnemyDead())
            {
                Context.NextStateCode = StateCode.PlayerWinState;
            }
        }

        public override void Select()
        {
            BaseState nextState = Context.NextStateCode == StateCode.Next
                ? _advanceTurnState
                : _turnStopState;
            Context.TransitionTo(nextState);
        }
    }
}