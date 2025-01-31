﻿using System;
using BattleScene.DataAccesses;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Presenters.Services;
using BattleScene.Views.ViewModels;
using BattleScene.Views.Views;
using R3;

namespace BattleScene.Presenters.ReactivePresenters
{
    public class AilmentViewReactivePresenter : IReactive<AilmentEntity>
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly ToIndexService _toIndex;
        private readonly EnemyGroupView _enemyGroupView;
        private readonly PlayerStatusView _playerAilmentsView;

        public AilmentViewReactivePresenter(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            ToIndexService toIndex,
            EnemyGroupView enemyGroupView,
            PlayerStatusView playerAilmentsView)
        {
            _characterRepository = characterRepository;
            _toIndex = toIndex;
            _enemyGroupView = enemyGroupView;
            _playerAilmentsView = playerAilmentsView;
        }

        public void Observe(AilmentEntity ailment)
        {
            var character = _characterRepository.Get(ailment.CharacterId);
            Action<bool> startAilmentViewAction = character.IsPlayer
                ? x => StartPlayerAilmentView(ailment.AilmentCode, x)
                : x => StartEnemyAilmentView(ailment.CharacterId, ailment.AilmentCode, x);
            ailment.ReactiveEffects.Subscribe(startAilmentViewAction);
        }

        private void StartPlayerAilmentView(AilmentCode ailmentCode, bool effects)
        {
            var ailmentId = _toIndex.FromAilment(ailmentCode);
            var dto = new AilmentViewModel(ailmentId, effects);
            _playerAilmentsView.StartAilmentAnimation(dto);
        }

        private void StartEnemyAilmentView(CharacterId characterId, AilmentCode ailmentCode, bool effects)
        {
            var position = _characterRepository.Get(characterId).Position;
            var ailmentId = _toIndex.FromAilment(ailmentCode);
            var dto = new AilmentViewModel(ailmentId, effects);
            _enemyGroupView[position].StartAilmentAnimation(dto);
        }
    }
}