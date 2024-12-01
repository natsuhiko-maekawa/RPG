using BattleScene.InterfaceAdapter.PresenterFacade;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    /// <summary>
    /// 特定の状態異常を回復するステート。<br/>
    /// prev : <see cref="TurnStartState"/><br/>
    /// next : <see cref="TurnStopState"/>
    /// </summary>
    public class ResetAilmentState : BaseState
    {
        private readonly ResetAilmentUseCase _useCase;
        private readonly ResetAilmentPresenterFacade _facade;
        private readonly TurnStopState _turnStopState;

        public ResetAilmentState(
            ResetAilmentUseCase useCase,
            ResetAilmentPresenterFacade facade,
            TurnStopState turnStopState)
        {
            _useCase = useCase;
            _facade = facade;
            _turnStopState = turnStopState;
        }

        public override void Start()
        {
            _useCase.Reset(Context.AilmentCode);
            Context.Skill = _useCase.GetAttackSkill();
            _facade.Output(Context.Skill.Common.SkillCode);
        }

        public override void Select()
        {
            Context.TransitionTo(_turnStopState);
        }
    }
}