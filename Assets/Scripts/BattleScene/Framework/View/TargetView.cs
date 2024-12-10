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

            if (IsEnemySolo(model.OptionTargetList))
            {
                if (_index == -1) SetIndex(model);

                _enemiesView[_enemyPositionList[_index]].StartFrameAnimationAsync(frameViewDto);
                return;
            }

            foreach (var character in model.OptionTargetList)
            {
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

        private bool IsEnemySolo(IReadOnlyList<Character> characterDtoList)
        {
            return characterDtoList.Count == 1 && !characterDtoList.Single().IsPlayer;
        }

        private void SetIndex(TargetViewModel model)
        {
            _enemyPositionList = _enemiesView
                .Where(x => x.enabled)
                .Select((_, i) => i)
                .ToList();
            var position = model.OptionTargetList.First().Position;
            _index = _enemyPositionList.First(x => x == position);
            Debug.Assert(_index != -1);
        }

        private void MoveFrame(Vector2 vector2)
        {
            if (vector2.x == 0) return;

            _enemiesView[_index].StopFrameAnimation();
            _index = vector2.x > 0
                ? Math.Min(_index + 1, _enemyPositionList.Count - 1)
                : Math.Max(_index - 1, 0);

            StartAnimation(_model);
        }

        private IReadOnlyList<Character> GetTargetDtoList()
        {
            // if (_dto == null) return Array.Empty<CharacterStruct>();
            return IsEnemySolo(_model.OptionTargetList)
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
            if (context.performed)
            {
                var vector2 = context.ReadValue<Vector2>();
                MoveFrame(vector2);
            }
        }
    }
}