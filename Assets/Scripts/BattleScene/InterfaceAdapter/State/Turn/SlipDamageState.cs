using BattleScene.InterfaceAdapter.Facade;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class SlipDamageState : BaseState
    {
        private readonly SlipUseCase _slip;
        private readonly SlipDamageOutputFacade _slipDamageOutput;
        private readonly TurnStopState _turnStopState;
        private AnimationQueue _animationQueue = null!;

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
            Context.Skill = _slip.GetSkillCode();
            Context.SkillCode = Context.Skill.SkillCommon.SkillCode;
            Context.TargetIdList = _slip.GetTargetList();
            _slip.Commit();

            var outputQueue = _slipDamageOutput.GetOutputQueue(Context.Skill);
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