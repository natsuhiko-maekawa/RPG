using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Framework.InputActions;
using BattleScene.Views.Code;
using BattleScene.Views.GameObjects;
using BattleScene.Views.InputActions;
using BattleScene.Views.ViewModels;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace BattleScene.Views.Views
{
    public class TableView : MonoBehaviour, BattleSceneInputAction.IBattleSceneActions
    {
        public int maxGridSize;
        private Window _window;
        private Table _table;
        private ArrowRight _arrowRight;
        private ArrowUp _arrowUp;
        private ArrowDown _arrowDown;
        private MessageView _messageView;
        private PlayerView _playerView;
        private TableViewModel _model;
        private Dictionary<ActionCode, RowState> _rowStateDictionary;
        private BattleSceneInputAction _inputAction;
        private ISelectRowAction _selectRowAction;

        [Inject]
        public void Construct(ISelectRowAction selectRowAction)
        {
            _selectRowAction = selectRowAction;
        }

        private void Awake()
        {
            _window = GetComponentInChildren<Window>();
            _table = GetComponentInChildren<Table>();
            _table.SetItem(maxGridSize);
            _arrowRight = GetComponentInChildren<ArrowRight>();
            _arrowUp = GetComponentInChildren<ArrowUp>();
            _arrowDown = GetComponentInChildren<ArrowDown>();
            var root = transform.root;
            _messageView = root.GetComponentInChildren<MessageView>();
            _playerView = root.GetComponentInChildren<PlayerView>();
            _inputAction = new BattleSceneInputAction();
            _inputAction.BattleScene.AddCallbacks(this);

            var actionCount = Enum.GetValues(typeof(ActionCode)).Length;
            _rowStateDictionary = new Dictionary<ActionCode, RowState>(actionCount);
        }

        public void StartAnimation(TableViewModel model)
        {
            enabled = true;
            _window.enabled = true;
            _inputAction.Enable();

            _model = model;
            if (!_rowStateDictionary.TryGetValue(model.ActionCode, out var gridState))
            {
                gridState = new RowState(
                    maxTableSize: maxGridSize,
                    itemCount: model.RowList.Count);
                _rowStateDictionary.Add(model.ActionCode, gridState);
            }

            _table.enabled = true;

            var rows = _table
                .Zip(model.RowList
                    .Select((rowModel, index) => (rowModel, index))
                    .Skip(gridState.TopItemIndex), (row, rowAndIndex) => (row, rowAndIndex.rowModel, rowAndIndex.index))
                .ToArray();

            SetRow(rows);

            _arrowRight.Move(gridState.SelectedRow);
            _arrowUp.enabled = gridState.IsHiddenUpper;
            _arrowDown.enabled = gridState.IsHiddenLower;

            _messageView.enabled = true;
            _messageView.StartAnimation(new MessageViewModel(
                message: model.RowList[gridState.SelectedIndex].RowDescription,
                noWait: true));

            _playerView.enabled = true;
            _playerView.StartAnimation(new PlayerViewModel(
                playerImagePath: model.RowList[gridState.SelectedIndex].PlayerImagePath));
            _playerView.StartSlideAnimation();
        }

        private void SetRow((Row row, RowModel rowModel, int index)[] rows)
        {
            foreach (var (row, rowModel, index) in rows)
            {
                row.SetName(rowModel.RowName);
                SetRowState(row, rowModel, index);
                row.ShowName();

                if (_model.ActionCode != ActionCode.Skill) continue;
                row.SetTechnicalPoint(rowModel.TechnicalPoint);
                row.ShowTechnicalPoint();
            }
        }

        private void SetRowState(Row row, RowModel rowModel, int index)
        {
            if (!rowModel.Enabled)
            {
                row.Inactive();
                return;
            }
            if (index == _rowStateDictionary[_model.ActionCode].SelectedIndex)
            {
                row.Highlight();
            }
            else
            {
                row.Unhighlight();
            }
        }

        public void StopAnimation()
        {
            enabled = false;
        }

        private void OnDisable()
        {
            _window.enabled = false;
            _table.enabled = false;
            _arrowRight.enabled = false;
            _arrowUp.enabled = false;
            _arrowDown.enabled = false;
            _inputAction.Disable();
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var rowState = _rowStateDictionary[_model.ActionCode];
                var index = rowState.SelectedIndex;
                if (!_model.RowList[index].Enabled) return;
                enabled = false;
                _selectRowAction.OnSelect(index);
            }
        }

        public void OnCancel(InputAction.CallbackContext context) { }

        public void OnMoveCursor(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var vector2 = context.ReadValue<Vector2>();
                MoveArrow(vector2);
            }
        }

        private void MoveArrow(Vector2 vector2)
        {
            if (vector2.y == 0) return;

            if (vector2.y > 0)
                _rowStateDictionary[_model.ActionCode].Up();
            else
                _rowStateDictionary[_model.ActionCode].Down();
            StartAnimation(_model);
        }
    }
}