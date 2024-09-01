using System;
using BattleScene.InterfaceAdapter.Controller;
using UnityEngine;
using VContainer;

namespace BattleScene.Framework.InputSystem
{
    public class GameLoop : MonoBehaviour, IGameLoop
    {
        // private SelectActionController _selectActionController;
        //
        // [Inject]
        // public void Construct(
        //     SelectActionController selectActionController)
        // {
        //     _selectActionController = selectActionController;
        // }

        private event Action StartEvent;
        
        private void Start()
        {
            StartEvent?.Invoke();
        }

        public void Subscribe(Action start)
        {
            StartEvent += start;
        }
    }
}