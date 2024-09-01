using System;
using BattleScene.InterfaceAdapter.IInputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleScene.Framework.InputSystem
{
    public class BattleSceneInput : MonoBehaviour, IBattleSceneInput
    {
        [SerializeField] private InputAction selectAction;
        [SerializeField] private InputAction cancelAction;

        public void SetSelectAction(Action action)
        {
            selectAction.performed += _ => action.Invoke();
            selectAction?.Enable();
        }

        public void SetCancelAction(Action action)
        {
            cancelAction.performed += _ => action.Invoke();
            cancelAction?.Enable();
        }
    }
}