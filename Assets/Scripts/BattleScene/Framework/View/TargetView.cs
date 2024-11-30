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
        private TargetViewDto _dto;
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

        public void StartAnimation(TargetViewDto dto)
        {
            enabled = true;
            _inputAction.Enable();

            _dto = dto;
            var frameViewDto = new FrameViewDto(Color.red);

            if (IsEnemySolo(dto.CharacterDtoList))
            {
                if (_index == -1) SetIndex(dto);

                _enemiesView[_enemyPositionList[_index]].StartFrameAnimationAsync(frameViewDto);
                return;
            }

            foreach (var character in dto.CharacterDtoList)
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
            _inputAction.Disable();
            _playerView.StopPlayerFrameView();
            foreach (var enemyView in _enemiesView)
            {
                enemyView.StopFrameAnimation();
            }

            enabled = false;
        }

        private bool IsEnemySolo(IReadOnlyList<CharacterStruct> characterDtoList)
        {
            return characterDtoList.Count == 1 && !characterDtoList.Single().IsPlayer;
        }

        private void SetIndex(TargetViewDto dto)
        {
            _enemyPositionList = _enemiesView
                .Where(x => x.enabled)
                .Select((_, i) => i)
                .ToList();
            var position = dto.CharacterDtoList.First().Position;
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

            StartAnimation(_dto);
        }

        private IReadOnlyList<CharacterStruct> GetTargetDtoList()
        {
            // if (_dto == null) return Array.Empty<CharacterStruct>();
            return IsEnemySolo(_dto.CharacterDtoList)
                ? new[] { CharacterStruct.CreateEnemy(_enemyPositionList[_index]) }
                : _dto.CharacterDtoList;
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