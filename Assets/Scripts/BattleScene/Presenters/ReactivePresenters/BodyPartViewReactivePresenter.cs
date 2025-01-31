﻿using BattleScene.DataAccesses;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Presenters.Services;
using BattleScene.Views.ViewModels;
using BattleScene.Views.Views;
using R3;

namespace BattleScene.Presenters.ReactivePresenters
{
    public class BodyPartViewReactivePresenter : IReactive<BodyPartEntity>
    {
        private readonly ToIndexService _toIndex;
        private readonly PlayerStatusView _playerStatusView;

        public BodyPartViewReactivePresenter(
            ToIndexService toIndex,
            PlayerStatusView playerStatusView)
        {
            _toIndex = toIndex;
            _playerStatusView = playerStatusView;
        }

        public void Observe(BodyPartEntity bodyPart)
        {
            bodyPart.ReactiveDestroyedCount.Subscribe(x => StartPlayerBodyPartView(bodyPart.BodyPartCode, x));
        }

        private void StartPlayerBodyPartView(BodyPartCode bodyPartCode, int destroyedCount)
        {
            var index = _toIndex.FromBodyPart(bodyPartCode);
            var model = new BodyPartViewModel(index, destroyedCount);
            _playerStatusView.StartDestroyAnimation(model);
        }
    }
}