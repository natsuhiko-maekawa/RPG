﻿using BattleScene.InterfaceAdapter.PresenterFacades;
using BattleScene.InterfaceAdapter.Presenters;
using BattleScene.UseCases.UseCases;

namespace BattleScene.InterfaceAdapter.States.Battle
{
    public class InitializeEnemyState : BaseState
    {
        private readonly InitializeEnemyUseCase _useCase;
        private readonly InitializeEnemyPresenterFacade _facade;
        private readonly EnemyImageViewPresenter _enemyImageView;
        private readonly TurnState _turnState;

        public InitializeEnemyState(
            InitializeEnemyUseCase useCase,
            InitializeEnemyPresenterFacade facade,
            EnemyImageViewPresenter enemyImageView,
            TurnState turnState)
        {
            _useCase = useCase;
            _facade = facade;
            _enemyImageView = enemyImageView;
            _turnState = turnState;
        }

        public override async void Start()
        {
            var enemyList = _useCase.CreateEnemy();
            _enemyImageView.StartAnimation(enemyList.Length);
            await _enemyImageView.SetImage(enemyList);
            _useCase.Initialize();
            _facade.Initialize(enemyList);
            Context.TransitionTo(_turnState);
        }
    }
}