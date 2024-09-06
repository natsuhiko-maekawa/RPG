using BattleScene.UseCases.View;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class PlayerSelectSkillState : AbstractState
    {
        private readonly SkillView _skillView;

        public PlayerSelectSkillState(
            SkillView skillView)
        {
            _skillView = skillView;
        }

        public override void Start()
        {
            _skillView.Start();
        }
    }
}