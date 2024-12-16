using BattleScene.Views.InputActions;
using BattleScene.Views.Views;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using static BattleScene.Views.InputActions.BattleSceneInputAction;

namespace BattleScene.Views.Inputs
{
    public class BattleSceneInput : MonoBehaviour, IBattleSceneActions
    {
        private BattleSceneInputAction _inputAction;
        private TableView _tableView;
        private TargetView _targetView;
        private INoArgumentActions _noArgumentActions;

        [Inject]
        public void Construct(
            TableView tableView,
            TargetView targetView,
            INoArgumentActions noArgumentActions)
        {
            _tableView = tableView;
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
            if (_tableView.enabled) return;
            if (_targetView.enabled) return;
            if (context.performed) _noArgumentActions.OnSelect();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (context.performed) _noArgumentActions.OnCancel();
        }

        public void OnMoveCursor(InputAction.CallbackContext context)
        {
        }
    }
}