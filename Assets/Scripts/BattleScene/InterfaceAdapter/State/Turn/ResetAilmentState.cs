using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Facade;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    /// <summary>
    /// 特定の状態異常を回復するステート。<br/>
    /// prev : <see cref="TurnStartState"/><br/>
    /// next : <see cref="TurnStopState"/>
    /// </summary>
    public class ResetAilmentState : BaseState
    {
        private readonly AilmentResetService _ailmentResetService;
        private readonly ResetAilmentOutputFacade _resetAilmentOutput;
        private readonly TurnStopState _turnStopState;

        public ResetAilmentState(
            AilmentResetService ailmentResetService,
            ResetAilmentOutputFacade resetAilmentOutput,
            TurnStopState turnStopState)
        {
            _ailmentResetService = ailmentResetService;
            _resetAilmentOutput = resetAilmentOutput;
            _turnStopState = turnStopState;
        }

        public override async void Start()
        {
            _ailmentResetService.Reset();
            Context.SkillCode = SkillCode.Attack;
            await _resetAilmentOutput.OutputAsync(Context.SkillCode);
        }

        public override void Select()
        {
            Context.TransitionTo(_turnStopState);
        }
    }
}