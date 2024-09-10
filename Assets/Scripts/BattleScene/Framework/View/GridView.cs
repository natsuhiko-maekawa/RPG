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
        public Grid Grid { get; private set; }
        private ArrowRight _arrowRight;
        private ArrowUp _arrowUp;
        private ArrowDown _arrowDown;
        public GridViewDto Dto { get; private set; }
        private int _id;
        private readonly Dictionary<ActionCode, GridState> _gridStateDictionary = new();

        private void Awake()
        {
            _window = GetComponentInChildren<Window>();
            Grid = GetComponentInChildren<Grid>();
            _arrowRight = GetComponentInChildren<ArrowRight>();
            _arrowUp = GetComponentInChildren<ArrowUp>();
            _arrowDown = GetComponentInChildren<ArrowDown>();
            SetMoveAction(MoveArrow);
        }

        public Task StartAnimationAsync(GridViewDto dto)
        {
            _window.Show();
            selectAction.Enable();
            
            Dto = dto;
            if (!_gridStateDictionary.TryGetValue(dto.ActionCode, out var gridState))
            {
                gridState = new GridState(
                    maxGridSize: maxGridSize,
                    itemCount: dto.RowDtoList.Count);
                _gridStateDictionary.Add(dto.ActionCode, gridState);
            }
            
            Grid.SetRow(Math.Min(dto.RowDtoList.Count, maxGridSize));
            foreach (var (row, rowDto) in Grid.Zip(dto.RowDtoList.Skip(gridState.TopItemIndex), (row, rowDto) => (row, rowDto)))
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
            
            return Task.CompletedTask;
        }

        public void StopAnimation()
        {
            _window.Hide();
            Grid.Reset();
            selectAction.Disable();
        }
        
        private async Task MoveArrow(Vector2 vector2)
        {
            if (vector2.y == 0) return;
            
            if (vector2.y > 0)
                _gridStateDictionary[Dto.ActionCode].Up();
            else
                _gridStateDictionary[Dto.ActionCode].Down();
            _id = _gridStateDictionary[Dto.ActionCode].SelectedRow;
            await StartAnimationAsync(Dto);
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