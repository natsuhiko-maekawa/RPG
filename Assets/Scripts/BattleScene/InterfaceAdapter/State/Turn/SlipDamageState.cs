using BattleScene.InterfaceAdapter.Facade;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class SlipDamageState : BaseState
    {
        private readonly SlipUseCase _slip;
        private readonly SlipDamageOutputFacade _slipDamageOutput;
        private readonly TurnStopState _turnStopState;
        private AnimationQueue? _animationQueue;

        public SlipDamageState(
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

            var outputQueue = _slipDamageOutput.GetOutputQueue(Context.SkillCode);
            _animationQueue = new AnimationQueue(outputQueue);
            _animationQueue.OnLastAnimate += TransitionState;
            await _animationQueue.Animate();
        }

        public override async void Select()
        {
            await _animationQueue.Animate();
        }

        private void TransitionState() => Context.TransitionTo(_turnStopState);
    }
}