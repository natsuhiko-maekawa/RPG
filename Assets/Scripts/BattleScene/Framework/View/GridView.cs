using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Framework.GameObjects;
using BattleScene.InterfaceAdapter.Code;
using BattleScene.InterfaceAdapter.Interface;
using BattleScene.InterfaceAdapter.Presenter.Dto;
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
            SetMoveAction(MoveArrow);
        }

        public Task StartAnimationAsync(GridViewDto dto)
        {
            _window.Show();
            _grid.SetRow(dto.RowDtoList.Count);
            foreach (var (row, rowDto) in _grid.Zip(dto.RowDtoList, (row, rowDto) => (row, rowDto)))
            {
                row.SetName(rowDto.RowName);
                row.ShowName();
            }
            
            _dto = dto;
            if (!_gridStateDictionary.TryGetValue(dto.ActionCode, out var gridState))
            {
                gridState = new GridState(
                    maxGridSize: maxGridSize,
                    itemCount: dto.RowDtoList.Count);
                _gridStateDictionary.Add(dto.ActionCode, gridState);
            }
            
            _arrowRight.Move(_id);
            _arrowUp.Show(gridState.IsHiddenLower);
            _arrowDown.Show(gridState.IsHiddenLower);
            
            return Task.CompletedTask;
        }

        public void StopAnimation()
        {
            _window.Hide();
            _grid.Reset();
        }
        
        private async Task MoveArrow(Vector2 vector2)
        {
            if (vector2.y == 0) return;
            
            if (vector2.y > 0)
                _gridStateDictionary[_dto.ActionCode].Up();
            else
                _gridStateDictionary[_dto.ActionCode].Down();
            var selectedRow = _gridStateDictionary[_dto.ActionCode].SelectedIndex;
            _id = _dto?.RowDtoList[selectedRow].RowId ?? 0;
            await StartAnimationAsync(_dto);
        }
        
        private void SetMoveAction(Func<Vector2, Task> func)
        {
            moveAction.performed += x => func.Invoke(x.ReadValue<Vector2>());
            moveAction?.Enable();
        }

        public void SetSelectAction(Action<int> action)
        {
            selectAction.performed += _ => action.Invoke(_id);
            selectAction?.Enable();
        }
    }
}