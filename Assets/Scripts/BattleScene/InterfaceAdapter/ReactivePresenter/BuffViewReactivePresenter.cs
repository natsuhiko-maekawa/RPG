using System;
using BattleScene.DataAccess;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.Service;
using R3;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.ReactivePresenter
{
    public class BuffViewReactivePresenter : IReactive<BuffEntity>
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly ToIndexService _toIndex;
        private readonly PlayerStatusView _playerStatusView;

        public BuffViewReactivePresenter(
            ToIndexService toIndex,
            PlayerStatusView playerStatusView,
            ICollection<CharacterEntity, CharacterId> characterCollection)
        {
            _toIndex = toIndex;
            _playerStatusView = playerStatusView;
            _characterCollection = characterCollection;
        }

        public void Observe(BuffEntity buff)
        {
            if (_characterCollection.Get(buff.CharacterId).IsPlayer)
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