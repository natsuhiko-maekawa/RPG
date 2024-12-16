using BattleScene.UseCases.Services;
using BattleScene.Views.ViewModels;
using BattleScene.Views.Views;

namespace BattleScene.Presenters.Presenters
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