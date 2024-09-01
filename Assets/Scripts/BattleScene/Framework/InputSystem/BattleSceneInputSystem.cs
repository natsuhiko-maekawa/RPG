using System;
using BattleScene.InterfaceAdapter.Controller;
using BattleScene.InterfaceAdapter.IInputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace BattleScene.Framework.InputSystem
{
    public class BattleSceneInputSystem : MonoBehaviour, IBattleSceneInputSystem
    {
        [SerializeField] private InputAction nextAction;
        [SerializeField] private InputAction cancelAction;
        [SerializeField] private InputAction selectAction;
        // private SelectActionController _selectActionController;
        //
        // [Inject]
        // public void Construct(
        //     SelectActionController selectActionController)
        // {
        //     _selectActionController = selectActionController;
        // }
        //
        // private void Awake()
        // {
        //     SetOnNextAction(_selectActionController.Select);
        // }

        public void SetOnNextAction(Action action)
        {
            nextAction.performed += _ => action.Invoke();
            nextAction?.Enable();
        }

        public void SetOnCancelAction(Action action)
        {
            cancelAction.performed += _ => action.Invoke();
            cancelAction?.Enable();
        }

        public void SetOnSelectAction(Action<Vector2> action)
        {
            selectAction.performed += x => action.Invoke(x.ReadValue<Vector2>());
            selectAction?.Enable();
        }
    }
}