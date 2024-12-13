using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Framework.InputActions;
using BattleScene.Framework.ViewModels;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace BattleScene.Framework.Views
{
    public class TargetView : MonoBehaviour, BattleSceneInputAction.IBattleSceneActions
    {
        private EnemyGroupView _enemyGroupView;
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
            _enemyGroupView = GetComponentInChildren<EnemyGroupView>();
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
            var frame = new FrameViewModel(Color.red);

            if (IsEnemySolo(model))
            {
                if (_index == -1) SetIndex(model);

                _enemyGroupView[_enemyPositionList[_index]].StartFrameAnimation(frame);
                return;
            }

            foreach (var index in model.SelectedTargetIndexList)
            {
                var character = model.OptionTargetList[index];

                if (character.IsPlayer)
                {
                    _playerView.StartFrameAnimation(frame);
                    continue;
                }

                _enemyGroupView[character.Position].StartFrameAnimation(frame);
            }
        }

        public void StopAnimation()
        {
            _index = -1;
            _inputAction.Disable();
            _playerView.StopFrameAnimation();
            foreach (var enemyView in _enemyGroupView)
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
            _enemyPositionList = _enemyGroupView
                .Select((enemyView, i) => (enemyView, i))
                .Where(x => x.enemyView.enabled)
                .Select(x => x.i)
                .ToArray();
            _index = model.SelectedTargetIndexList.Count == 1 ? model.SelectedTargetIndexList.Single() : -1;
        }

        private void MoveFrame(Vector2 vector2)
        {
            if (vector2.x == 0) return;

            _enemyGroupView[_enemyPositionList[_index]].StopFrameAnimation();
            _index = vector2.x > 0
                ? Math.Min(_index + 1, _enemyPositionList.Count - 1)
                : Math.Max(_index - 1, 0);

            StartAnimation(_model);
        }

        private IReadOnlyList<CharacterModel> GetTargetDtoList()
        {
            return IsEnemySolo(_model)
                ? new[] { CharacterModel.CreateEnemy(_enemyPositionList[_index]) }
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