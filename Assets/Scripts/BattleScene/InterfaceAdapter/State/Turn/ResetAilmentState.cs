using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Facade;
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
        private readonly ResetAilmentOutputFacade _output;
        private readonly TurnStopState _turnStopState;

        public ResetAilmentState(
            ResetAilmentUseCase useCase,
            ResetAilmentOutputFacade output,
            TurnStopState turnStopState)
        {
            _useCase = useCase;
            _output = output;
            _turnStopState = turnStopState;
        }

        public override async void Start()
        {
            _useCase.Reset();
            Context.Skill = _useCase.GetAttackSkill();
            Context.SkillCode = SkillCode.Attack;
            await _output.OutputAsync(Context.SkillCode);
        }

        public override void Select()
        {
            Context.TransitionTo(_turnStopState);
        }
    }
}