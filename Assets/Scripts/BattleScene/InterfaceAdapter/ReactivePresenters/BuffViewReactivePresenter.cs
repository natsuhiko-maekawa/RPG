using System;
using BattleScene.DataAccess;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Framework.ViewModels;
using BattleScene.Framework.Views;
using BattleScene.InterfaceAdapter.Services;
using R3;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.ReactivePresenters
{
    public class BuffViewReactivePresenter : IReactive<BuffEntity>
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly ToIndexService _toIndex;
        private readonly PlayerStatusView _playerStatusView;

        public BuffViewReactivePresenter(
            ToIndexService toIndex,
            PlayerStatusView playerStatusView,
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _toIndex = toIndex;
            _playerStatusView = playerStatusView;
            _characterRepository = characterRepository;
        }

        public void Observe(BuffEntity buff)
        {
            if (_characterRepository.Get(buff.CharacterId).IsPlayer)
                buff.ReactiveRate.Subscribe(x => StartPlayerBuffView(buff.BuffCode, x));
        }

        private void StartPlayerBuffView(BuffCode buffCode, float rate)
        {
            var index = _toIndex.FromBuff(buffCode);
            // バフの倍率の対数を求め、倍率が1より大きい場合正の整数を、倍率が1より小さい場合負の整数を、倍率が1の場合は0を返す。
            // ただし浮動小数点数を使用しているため、1を想定しているにもかかわらず、わずかに1より前後している可能性がある。
            // そのため、Mathf.Approximatelyメソッドを用いて、倍率がほぼ1である場合、三項演算子でsbyte型の0を返している。
            var buffState = (BuffState)(Mathf.Approximately(rate, 1.0f) ? 0 : (sbyte)Math.Sign(Mathf.Log(rate)));
            var buff = new BuffViewModel(index, buffState);
            _playerStatusView.StartPlayerBuffView(buff);
        }
    }
}