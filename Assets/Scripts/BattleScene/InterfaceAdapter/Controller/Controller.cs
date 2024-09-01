using System;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.IInputSystem;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCases;
using BattleScene.UseCases.IController;
using VContainer.Unity;

namespace BattleScene.InterfaceAdapter.Controller
{
    public class Controller : IInitializable
    {
        private readonly StateMachine _stateMachine;
        private readonly IBattleSceneInputSystem _battleSceneInputSystem;
        private readonly IGridView _gridView;

        public Controller(
            StateMachine stateMachine,
            IBattleSceneInputSystem battleSceneInputSystem,
            IGridView gridView)
        {
            _stateMachine = stateMachine;
            _battleSceneInputSystem = battleSceneInputSystem;
            _gridView = gridView;
        }

        void IInitializable.Initialize()
        {
            _battleSceneInputSystem.SetOnNextAction(_stateMachine.Select);
            _gridView.SetSelectAction(x => _stateMachine.SelectAction((ActionCode)x));
        }
    }
}