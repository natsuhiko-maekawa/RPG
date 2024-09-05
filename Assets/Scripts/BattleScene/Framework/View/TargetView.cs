using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleScene.Framework.View
{
    public class TargetView : MonoBehaviour, ITargetView
    {
        [SerializeField] private InputAction moveAction;
        [SerializeField] private InputAction selectAction;
        private EnemiesView _enemiesView;
        private PlayerView _playerView;
        private TargetViewDto _dto;
        private int _index = -1;
        private ImmutableList<int> _enemyPositionList;

        private void Start()
        {
            _enemiesView = GetComponentInChildren<EnemiesView>();
            _playerView = GetComponentInChildren<PlayerView>();
            SetMoveAction(MoveFrame);
        }

        public Task StartAnimation(TargetViewDto dto)
        {
            moveAction?.Enable();
            selectAction?.Enable();
            
            _dto = dto;
            var frameViewDto = new FrameViewDto(Color.red);

            if (IsEnemySolo(dto.CharacterDtoList))
            {
                if (_index == -1) SetIndex(dto);
                
                _enemiesView[_enemyPositionList[_index]].StartFrameAnimationAsync(frameViewDto);
                return Task.CompletedTask;
            }
            
            foreach (var character in dto.CharacterDtoList)
            {
                if (character.IsPlayer)
                {
                    _playerView.StartFrameView(frameViewDto);
                    continue;
                }
                
                _enemiesView[character.EnemyIndex].StartFrameAnimationAsync(frameViewDto);
            }
            
            return Task.CompletedTask;
        }

        public void StopAnimation()
        {
            moveAction?.Disable();
            selectAction?.Disable();
            _playerView.StopPlayerFrameView();
            foreach (var enemyView in _enemiesView)
            {
                enemyView.StopFrameAnimation();
            }
        }

        private bool IsEnemySolo(IList<CharacterDto> characterDtoList)
        {
            return characterDtoList.Count == 1 && !characterDtoList.First().IsPlayer;
        }
        
        private void SetIndex(TargetViewDto dto)
        {
            _enemyPositionList = _enemiesView
                .Where(x => x.enabled)
                .Select((_, i) => i)
                .ToImmutableList();
            var position = dto.CharacterDtoList.First().EnemyIndex;
            _index = _enemyPositionList.FindIndex(x => x == position);
            Debug.Assert(_index != -1);
        }

        private async Task MoveFrame(Vector2 vector2)
        {
            if (vector2.x == 0) return;

            _enemiesView[_index].StopFrameAnimation();
            _index = vector2.x > 0 
                ? Math.Min(_index + 1, _enemyPositionList.Count - 1) 
                : Math.Max(_index - 1, 0);
            
            await StartAnimation(_dto);
        }
        
        private void SetMoveAction(Func<Vector2, Task> func)
        {
            moveAction.performed += x => func.Invoke(x.ReadValue<Vector2>());
        }
        
        public void SetSelectAction(Action<ImmutableList<CharacterDto>> action)
        {
            selectAction.performed += _ => action.Invoke(GetTargetDtoList());
        }

        private ImmutableList<CharacterDto> GetTargetDtoList()
        {
            if (_dto == null) return ImmutableList<CharacterDto>.Empty;
            return IsEnemySolo(_dto.CharacterDtoList)
                ? ImmutableList.Create(new CharacterDto(_enemyPositionList[_index]))
                : _dto.CharacterDtoList;
        }
    }
}