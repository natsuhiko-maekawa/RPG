using System;
using BattleScene.InterfaceAdapter.IInputSystem;
using BattleScene.UseCase.IController;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.Controller
{
    public class BattleSceneController : IBattleSceneController
    {
        private readonly IBattleSceneInputSystem _battleSceneInputSystem;

        public BattleSceneController(
            IBattleSceneInputSystem battleSceneInputSystem)
        {
            _battleSceneInputSystem = battleSceneInputSystem;
        }
        
        public void SetOnNextAction(Action action)
        {
            _battleSceneInputSystem.SetOnNextAction(action);
        }

        public void SetOnCancelAction(Action action)
        {
            _battleSceneInputSystem.SetOnCancelAction(action);
        }

        public void SetOnSelectAction(Action<Vector2> action)
        {
            _battleSceneInputSystem.SetOnSelectAction(action);
        }
    }
}