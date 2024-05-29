using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCase.View.AttackCountView.OutputBoundary;
using BattleScene.UseCase.View.AttackCountView.OutputData;

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