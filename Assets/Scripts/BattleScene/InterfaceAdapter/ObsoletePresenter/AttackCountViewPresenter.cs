using BattleScene.Framework.ViewModel;
using BattleScene.UseCases.View.AttackCountView.OutputBoundary;
using BattleScene.UseCases.View.AttackCountView.OutputData;

namespace BattleScene.InterfaceAdapter.ObsoletePresenter
{
    public class AttackCountViewPresenter : IAttackCountViewPresenter
    {
        private readonly Framework.View.PlayerAttackCountView _playerAttackCountView;

        public AttackCountViewPresenter(
            Framework.View.PlayerAttackCountView playerAttackCountView)
        {
            _playerAttackCountView = playerAttackCountView;
        }

        public void Start(AttackCountOutputData attackCountOutputData)
        {
            _playerAttackCountView.StartAnimation(new PlayerAttackCountViewDto(attackCountOutputData.Rate));
        }
    }
}