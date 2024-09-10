using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Framework.GameObjects;
using BattleScene.InterfaceAdapter.Code;
using BattleScene.InterfaceAdapter.Interface;
using BattleScene.InterfaceAdapter.Presenter.Dto;
using BattleScene.InterfaceAdapter.Presenter.MessageView;
using UnityEngine;
using UnityEngine.InputSystem;
using Grid = BattleScene.Framework.GameObjects.Grid;

namespace BattleScene.Framework.View
{
    public class GridView : MonoBehaviour, IGridView
    {
        [SerializeField] private int maxGridSize;
        [SerializeField] private InputAction moveAction;
        [SerializeField] private InputAction selectAction;
        private Window _window;
        private Grid _grid;
        private ArrowRight _arrowRight;
        private ArrowUp _arrowUp;
        private ArrowDown _arrowDown;
        private MessageView _messageView;
        private GridViewDto _dto;
        private int _id;
        private readonly Dictionary<ActionCode, GridState> _gridStateDictionary = new();

        private void Awake()
        {
            _window = GetComponentInChildren<Window>();
            _grid = GetComponentInChildren<Grid>();
            _arrowRight = GetComponentInChildren<ArrowRight>();
            _arrowUp = GetComponentInChildren<ArrowUp>();
            _arrowDown = GetComponentInChildren<ArrowDown>();
            // TODO: もっといいGetComponentの方法があるかも
            _messageView = transform.parent.GetComponentInChildren<MessageView>();
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
            
            _grid.SetRow(Math.Min(dto.RowDtoList.Count, maxGridSize));
            foreach (var (row, rowDto) in _grid.Zip(dto.RowDtoList.Skip(gridState.TopItemIndex), (row, rowDto) => (row, rowDto)))
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
            await _messageView.StartAnimation(new MessageViewDto(
                Message: dto.RowDtoList[gridState.SelectedIndex].RowDescription,
                NoWait: true));
        }

        public void StopAnimation()
        {
            _window.Hide();
            _grid.ResetRow();
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
            _id = _gridStateDictionary[_dto.ActionCode].SelectedRow;
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