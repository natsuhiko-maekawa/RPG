using System;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.IInputSystem;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCases;
using BattleScene.UseCases.IController;

namespace BattleScene.InterfaceAdapter.Controller
{
    public class Controller// : IController
    {
        private readonly IBattleSceneInputSystem _battleSceneInputSystem;
        private readonly IGameLoop _gameLoop;

        public Controller(
            IBattleSceneInputSystem battleSceneInputSystem,
            IGameLoop gameLoop)
        {
            _battleSceneInputSystem = battleSceneInputSystem;
            _gameLoop = gameLoop;
        }

        public void Subscribe(StateMachine stateMachine)
        {
            _battleSceneInputSystem.SetOnNextAction(stateMachine.Select);
        }
    }
}