using BattleScene.InterfaceAdapter.PresenterFacade;
using BattleScene.UseCases.UseCase;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class SlipDamageState : BaseState
    {
        private readonly SlipUseCase _slip;
        private readonly SlipDamagePresenterFacade _facade;
        private readonly TurnStopState _turnStopState;
        private AnimationQueue _animationQueue = null!;

        public SlipDamageState(
            SlipUseCase slip,
            SlipDamagePresenterFacade facade,
            TurnStopState turnStopState)
        {
            _slip = slip;
            _facade = facade;
            _turnStopState = turnStopState;
        }

        public override void Start()
        {
            Context.Skill = _slip.GetSkillCode();
            Context.TargetIdList = _slip.GetTargetList();
            _slip.Commit(Context.SlipCode);

            var outputQueue = _facade.GetOutputQueue(Context.Skill);
            _animationQueue = new AnimationQueue(outputQueue);
            _animationQueue.OnLastAnimate += TransitionState;
            _animationQueue.Animate();
        }

        public override void Select()
        {
            _animationQueue.Animate();
        }

        private void TransitionState() => Context.TransitionTo(_turnStopState);
    }
}