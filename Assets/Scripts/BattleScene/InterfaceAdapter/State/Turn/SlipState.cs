using BattleScene.InterfaceAdapter.Facade;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    internal class SlipState : BaseState
    {
        private readonly SlipUseCase _slip;
        private readonly SlipDamageOutputFacade _slipDamageOutput;
        private readonly TurnStopState _turnStopState;

        public SlipState(
            SlipUseCase slip,
            SlipDamageOutputFacade slipDamageOutput,
            TurnStopState turnStopState)
        {
            _slip = slip;
            _slipDamageOutput = slipDamageOutput;
            _turnStopState = turnStopState;
        }

        public override async void Start()
        {
            Context.SkillCode = _slip.GetSkillCode();
            Context.TargetIdList = _slip.GetTargetList();
            _slip.Commit();
            await _slipDamageOutput.Output(Context.SkillCode);
        }

        public override void Select()
        {
            Context.TransitionTo(_turnStopState);
        }
    }
}