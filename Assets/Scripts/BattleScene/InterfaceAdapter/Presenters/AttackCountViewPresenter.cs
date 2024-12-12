using BattleScene.Framework.ViewModels;
using BattleScene.Framework.Views;
using BattleScene.UseCases.Service;

namespace BattleScene.InterfaceAdapter.Presenters
{
    public class AttackCountViewPresenter
    {
        private readonly AttackCounterService _attackCounter;
        private readonly PlayerAttackCountView _playerAttackCountView;

        public AttackCountViewPresenter(
            AttackCounterService attackCounter,
            PlayerAttackCountView playerAttackCountView)
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