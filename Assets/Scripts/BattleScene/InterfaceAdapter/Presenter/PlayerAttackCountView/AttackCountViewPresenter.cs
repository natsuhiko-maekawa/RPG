using BattleScene.InterfaceAdapter.Interface;
using BattleScene.UseCases.View.AttackCountView.OutputBoundary;
using BattleScene.UseCases.View.AttackCountView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.PlayerAttackCountView
{
    public class AttackCountViewPresenter : IAttackCountViewPresenter
    {
        private readonly IPlayerAttackCountView _playerAttackCountView;

        public AttackCountViewPresenter(
            IPlayerAttackCountView playerAttackCountView)
        {
            _playerAttackCountView = playerAttackCountView;
        }

        public void Start(AttackCountOutputData attackCountOutputData)
        {
            _playerAttackCountView.StartAnimation(new PlayerAttackCountViewDto(attackCountOutputData.Rate));
        }
    }
}