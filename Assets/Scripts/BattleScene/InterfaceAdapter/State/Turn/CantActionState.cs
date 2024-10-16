using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.State.Turn
{
    public class CantActionState : BaseState
    {
        private readonly CantActionService _cantAction;
        private readonly SkillState _skillState;

        public CantActionState(
            CantActionService cantAction,
            SkillState skillState)
        {
            _cantAction = cantAction;
            _skillState = skillState;
        }

        public override void Start()
        {
            Context.SkillCode = _cantAction.ToSkillCode();
            Context.TargetIdList = _cantAction.TargetIdList;
            Context.TransitionTo(_skillState);
        }
    }
}