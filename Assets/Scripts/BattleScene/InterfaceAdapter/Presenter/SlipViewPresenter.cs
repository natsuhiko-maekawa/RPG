﻿using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.Service;
using R3;

namespace BattleScene.InterfaceAdapter.Presenter
{
    internal class SlipViewPresenter : DataAccess.IObserver<SlipEntity>
    {
        private readonly ToIndexService _toIndex;
        private readonly PlayerStatusView _playerAilmentsView;

        public SlipViewPresenter(
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

        private void StartPlayerAilmentView(SlipDamageCode slipDamageCode, bool effects)
        {
            var slipId = _toIndex.FromSlipDamage(slipDamageCode);
            var ailment = new AilmentViewModel(slipId, effects);
            _playerAilmentsView.StartPlayerAilmentsView(ailment);
        }
    }
}