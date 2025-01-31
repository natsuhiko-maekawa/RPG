﻿using BattleScene.DataAccesses;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.Views.ViewModels;
using BattleScene.Views.Views;
using R3;

namespace BattleScene.Presenters.ReactivePresenters
{
    public class TechnicalPointViewReactivePresenter : IReactive<CharacterEntity>
    {
        private readonly IFactory<PlayerPropertyValueObject, CharacterTypeCode> _playerPropertyFactory;
        private readonly PlayerView _playerView;

        public TechnicalPointViewReactivePresenter(
            IFactory<PlayerPropertyValueObject, CharacterTypeCode> playerPropertyFactory,
            PlayerView playerView)
        {
            _playerPropertyFactory = playerPropertyFactory;
            _playerView = playerView;
        }

        public void Observe(CharacterEntity character)
        {
            if (!character.IsPlayer) return;
            character.ReactiveCurrentTechnicalPoint.Subscribe(StartTechnicalPointBarView);
        }

        private void StartTechnicalPointBarView(int currentTechnicalPoint)
        {
            var maxTechnicalPoint = _playerPropertyFactory.Create(CharacterTypeCode.Player).TechnicalPoint;
            var model = new StatusBarViewModel(maxTechnicalPoint, currentTechnicalPoint);
            _playerView.StartTechnicalPointBarAnimation(model);
        }
    }
}