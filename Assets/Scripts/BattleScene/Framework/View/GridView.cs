using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Framework.Code;
using BattleScene.Framework.GameObjects;
using BattleScene.Framework.ViewModel;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleScene.Framework.View
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private int maxGridSize;
        [SerializeField] private InputAction moveAction;
        [SerializeField] private InputAction selectAction;
        private Window _window;
        private Table _grid;
        private ArrowRight _arrowRight;
        private ArrowUp _arrowUp;
        private ArrowDown _arrowDown;
        private MessageView _messageView;
        private PlayerView _playerView;
        private GridViewDto _dto;
        private readonly Dictionary<ActionCode, GridState> _gridStateDictionary = new();

        private void Awake()
        {
            _window = GetComponentInChildren<Window>();
            _grid = GetComponentInChildren<Table>();
            _grid.SetItem(maxGridSize);
            _arrowRight = GetComponentInChildren<ArrowRight>();
            _arrowUp = GetComponentInChildren<ArrowUp>();
            _arrowDown = GetComponentInChildren<ArrowDown>();
            // TODO: もっといいGetComponentの方法があるかも
            var root = transform.root;
            _messageView = root.GetComponentInChildren<MessageView>();
            _playerView = root.GetComponentInChildren<PlayerView>();
            SetMoveAction(MoveArrow);
        }

        public async Task StartAnimationAsync(GridViewDto dto)
        {
            _window.Show();
            selectAction.Enable();

            _dto = dto;
            if (!_gridStateDictionary.TryGetValue(dto.ActionCode, out var gridState))
            {
                gridState = new GridState(
                    maxGridSize: maxGridSize,
                    itemCount: dto.RowDtoList.Count);
                _gridStateDictionary.Add(dto.ActionCode, gridState);
            }

            _grid.SetActive();

            foreach (var (row, rowDto) in _grid.Zip(dto.RowDtoList.Skip(gridState.TopItemIndex),
                         (row, rowDto) => (row, rowDto)))
            {
                row.SetName(rowDto.RowName);
                row.ShowName();

                if (dto.ActionCode != ActionCode.Skill) continue;
                row.SetTechnicalPoint(rowDto.TechnicalPoint);
                row.ShowTechnicalPoint();
            }

            _arrowRight.Move(gridState.SelectedRow);
            _arrowUp.Show(gridState.IsHiddenUpper);
            _arrowDown.Show(gridState.IsHiddenLower);

            _messageView.enabled = true;
            await _messageView.StartAnimationAsync(new MessageViewDto(
                Message: dto.RowDtoList[gridState.SelectedIndex].RowDescription,
                NoWait: true));

            _playerView.enabled = true;
            await _playerView.StartAnimation(new PlayerViewDto(
                PlayerImage: dto.RowDtoList[gridState.SelectedIndex].PlayerImagePath));
        }

        public void StopAnimation()
        {
            _window.Hide();
            _grid.SetInActive();
            _arrowRight.Hide();
            _arrowUp.Hide();
            _arrowDown.Hide();
            selectAction.Disable();
        }

        private async Task MoveArrow(Vector2 vector2)
        {
            if (vector2.y == 0) return;

            if (vector2.y > 0)
                _gridStateDictionary[_dto.ActionCode].Up();
            else
                _gridStateDictionary[_dto.ActionCode].Down();
            await StartAnimationAsync(_dto);
        }

        private void SetMoveAction(Func<Vector2, Task> func)
        {
            moveAction.performed += x => func.Invoke(x.ReadValue<Vector2>());
            moveAction?.Enable();
        }

        public void SetSelectAction(Action<int> action)
        {
            selectAction.performed += _ => action.Invoke(_gridStateDictionary[_dto.ActionCode].SelectedIndex);
            selectAction?.Enable();
        }
    }
}