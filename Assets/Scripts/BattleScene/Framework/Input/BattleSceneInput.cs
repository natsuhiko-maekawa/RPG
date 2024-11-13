using BattleScene.Framework.InputActions;
using BattleScene.Framework.View;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using static BattleScene.Framework.InputActions.BattleSceneInputAction;

namespace BattleScene.Framework.Input
{
    public class BattleSceneInput : MonoBehaviour, IBattleSceneActions
    {
        private BattleSceneInputAction _inputAction;
        private GridView _gridView;
        private TargetView _targetView;
        private INoArgumentActions _noArgumentActions;

        [Inject]
        public void Construct(
            GridView gridView,
            TargetView targetView,
            INoArgumentActions noArgumentActions)
        {
            _gridView = gridView;
            _targetView = targetView;
            _noArgumentActions = noArgumentActions;
        }

        private void Awake()
        {
            _inputAction = new BattleSceneInputAction();
            _inputAction.BattleScene.AddCallbacks(this);
            _inputAction.Enable();
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            if (_gridView.enabled) return;
            if (_targetView.enabled) return;
            if (context.performed) _noArgumentActions.OnSelect();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (context.performed) _noArgumentActions.OnCancel();
        }

        public void OnMoveCursor(InputAction.CallbackContext context) { }
    }
}