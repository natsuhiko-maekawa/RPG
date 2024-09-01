using System;
using BattleScene.InterfaceAdapter.IInput;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleScene.Framework.Input
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