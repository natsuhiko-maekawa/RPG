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
            // QUESTION: Mathf.Logに1であることが期待される浮動小数点数を渡す場合、常に0を返す保証はあるか
            // バフの倍率の対数を求め、倍率が1より大きい場合正の整数を、倍率が1より小さい場合負の整数を、倍率が1の場合は0を返す。
            // ただし浮動小数点数を使用しているため、1を想定しているにもかかわらず、わずかに1より前後している可能性がある。
            // そのため、Mathf.Approximatelyメソッドを用いて、倍率がほぼ1である場合、三項演算子でint型の0を返している。
            var normalizeRate = Mathf.Approximately(rate, 1.0f) ? 0 : Mathf.Log(rate);
            var buff = new BuffViewModel(index, Math.Sign(normalizeRate));
            _playerStatusView.StartPlayerBuffView(buff);
        }
    }
}