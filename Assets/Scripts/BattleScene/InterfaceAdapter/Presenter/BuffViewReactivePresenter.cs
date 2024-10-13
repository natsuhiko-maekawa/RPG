using System;
using BattleScene.DataAccess;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.Service;
using UnityEngine;
using R3;

namespace BattleScene.InterfaceAdapter.Presenter
{
    internal class BuffViewReactivePresenter : IReactive<BuffEntity>
    {
        private readonly ToIndexService _toIndex;
        private readonly PlayerStatusView _playerStatusView;

        public BuffViewReactivePresenter(
            ToIndexService toIndex,
            PlayerStatusView playerStatusView)
        {
            _toIndex = toIndex;
            _playerStatusView = playerStatusView;
        }

        public void Observe(BuffEntity buff)
        {
            buff.ReactiveRate.Subscribe(x => StartPlayerBuffView(buff.BuffCode, x));
        }
        
        private void StartPlayerBuffView(BuffCode buffCode, float rate)
        {
            var index = _toIndex.FromBuff(buffCode);
            var buff = new BuffViewModel(index, Math.Sign(Mathf.Log(rate)));
            _playerStatusView.StartPlayerBuffView(buff);
        }
    }
}