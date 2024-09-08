using System;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Framework.GameObjects;
using BattleScene.InterfaceAdapter.Interface;
using BattleScene.InterfaceAdapter.Presenter.Dto;
using UnityEngine;
using UnityEngine.InputSystem;
using Grid = BattleScene.Framework.GameObjects.Grid;

namespace BattleScene.Framework.View
{
    public class GridView : MonoBehaviour, IGridView
    {
        [SerializeField] private InputAction selectAction;
        private Window _window;
        private Grid _grid;

        private void Awake()
        {
            _window = GetComponentInChildren<Window>();
            _grid = GetComponentInChildren<Grid>();
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
            
            return Task.CompletedTask;
        }

        public void StopAnimation()
        {
            throw new NotImplementedException();
        }

        public void SetSelectAction(Action<int> action)
        {
            selectAction.performed += _ => action.Invoke(0);
            selectAction?.Enable();
        }
    }
}