using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Facade;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Turn
{
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