using BattleScene.DataAccess;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Framework.ViewModels;
using BattleScene.Framework.Views;
using BattleScene.InterfaceAdapter.Services;
using R3;

namespace BattleScene.InterfaceAdapter.ReactivePresenters
{
    public class SlipViewReactivePresenter : IReactive<SlipEntity>
    {
        private readonly ToIndexService _toIndex;
        private readonly PlayerStatusView _playerAilmentsView;

        public SlipViewReactivePresenter(
            ToIndexService toIndex,
            PlayerStatusView playerAilmentsView)
        {
            _toIndex = toIndex;
            _playerAilmentsView = playerAilmentsView;
        }

        public void Observe(SlipEntity slip)
        {
            slip.ReactiveEffects.Subscribe(x => StartPlayerAilmentView(slip.Id, x));
        }

        private void StartPlayerAilmentView(SlipCode slipCode, bool effects)
        {
            var slipId = _toIndex.FromSlipDamage(slipCode);
            var ailment = new AilmentViewModel(slipId, effects);
            _playerAilmentsView.StartAilmentAnimation(ailment);
        }
    }
}