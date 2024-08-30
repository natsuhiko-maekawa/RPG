﻿using BattleScene.Domain.DomainService;
using VContainer;

namespace BattleScene.UseCases.StateMachine
{
    internal class InitializePlayerState : AbstractState
    {
        private readonly PlayerDomainService _player;
        private readonly IObjectResolver _container;

        public InitializePlayerState(
            PlayerDomainService player,
            IObjectResolver container)
        {
            _player = player;
            _container = container;
        }

        public override void Start()
        {
            _player.Add();
            Context.TransitionTo(_container.Resolve<InitializeEnemyState>());
        }
    }
}