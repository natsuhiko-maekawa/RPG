using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Framework.InputActions;
using BattleScene.Framework.ViewModel;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace BattleScene.Framework.View
{
    public class TargetView : MonoBehaviour, BattleSceneInputAction.IBattleSceneActions
    {
        private EnemiesView _enemiesView;
        private PlayerView _playerView;
        private TargetViewModel _model;
        private int _index = -1;
        private IReadOnlyList<int> _enemyPositionList;
        private BattleSceneInputAction _inputAction;
        private ISelectTargetAction _selectTargetAction;

        [Inject]
        public void Construct(ISelectTargetAction selectTargetAction)
        {
            _selectTargetAction = selectTargetAction;
        }

        private void Start()
        {
            _enemiesView = GetComponentInChildren<EnemiesView>();
            _playerView = GetComponentInChildren<PlayerView>();
            _inputAction = new BattleSceneInputAction();
            _inputAction.BattleScene.AddCallbacks(this);
            enabled = false;
        }

        public void StartAnimation(TargetViewModel model)
        {
            enabled = true;
            _inputAction.Enable();

            _model = model;
            var frameViewDto = new FrameViewDto(Color.red);

            if (IsEnemySolo(model))
            {
                if (_index == -1) SetIndex(model);

                _enemiesView[_enemyPositionList[_index]].StartFrameAnimationAsync(frameViewDto);
                return;
            }

            foreach (var index in model.SelectedTargetIndexList)
            {
                var character = model.OptionTargetList[index];

                if (character.IsPlayer)
                {
                    _playerView.StartFrameView(frameViewDto);
                    continue;
                }

                _enemiesView[character.Position].StartFrameAnimationAsync(frameViewDto);
            }
        }

        public void StopAnimation()
        {
            _index = -1;
            _inputAction.Disable();
            _playerView.StopPlayerFrameView();
            foreach (var enemyView in _enemiesView)
            {
                enemyView.StopFrameAnimation();
            }

            enabled = false;
        }

        private bool IsEnemySolo(TargetViewModel model)
        {
            return model.SelectedTargetIndexList.Count == 1 && model.OptionTargetList.All(x => !x.IsPlayer);
        }

        private void SetIndex(TargetViewModel model)
        {
            _enemyPositionList = _enemiesView
                .Select((enemyView, i) => (enemyView, i))
                .Where(x => x.enemyView.enabled)
                .Select(x => x.i)
                .ToArray();
            _index = model.SelectedTargetIndexList.Count == 1 ? model.SelectedTargetIndexList.Single() : -1;
        }

        private void MoveFrame(Vector2 vector2)
        {
            if (vector2.x == 0) return;

            _enemiesView[_enemyPositionList[_index]].StopFrameAnimation();
            _index = vector2.x > 0
                ? Math.Min(_index + 1, _enemyPositionList.Count - 1)
                : Math.Max(_index - 1, 0);

            StartAnimation(_model);
        }

        private IReadOnlyList<Character> GetTargetDtoList()
        {
            return IsEnemySolo(_model)
                ? new[] { Character.CreateEnemy(_enemyPositionList[_index]) }
                : _model.OptionTargetList;
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var targetDtoList = GetTargetDtoList();
                _selectTargetAction.OnSelect(targetDtoList);
            }
        }

        public void OnCancel(InputAction.CallbackContext context) { }

        public void OnMoveCursor(InputAction.CallbackContext context)
        {
            if (!IsEnemySolo(_model)) return;
            if (context.performed)
            {
                var vector2 = context.ReadValue<Vector2>();
                MoveFrame(vector2);
            }
        }
    }
}