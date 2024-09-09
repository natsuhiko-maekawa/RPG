using BattleScene.UseCases.IPresenter;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class PlayerSelectSkillState : AbstractState
    {
        private readonly ISkillViewPresenter _skillView;

        public PlayerSelectSkillState(
            ISkillViewPresenter skillView)
        {
            _skillView = skillView;
        }

        public override void Start()
        {
            _skillView.Start();
        }
    }
}