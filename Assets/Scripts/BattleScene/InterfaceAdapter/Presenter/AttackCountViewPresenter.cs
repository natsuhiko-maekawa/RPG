using BattleScene.Framework.ViewModel;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class AttackCountViewPresenter
    {
        private readonly AttackCounterService _attackCounter;
        private readonly Framework.View.PlayerAttackCountView _playerAttackCountView;

        public AttackCountViewPresenter(
            AttackCounterService attackCounter,
            Framework.View.PlayerAttackCountView playerAttackCountView)
        {
            _attackCounter = attackCounter;
            _playerAttackCountView = playerAttackCountView;
        }

        public void Start()
        {
            var rate = _attackCounter.GetRate();
            var model = new AttackCountViewModel(rate);
            _playerAttackCountView.StartAnimation(model);
        }
    }
}