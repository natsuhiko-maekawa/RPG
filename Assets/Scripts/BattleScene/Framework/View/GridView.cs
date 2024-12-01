﻿using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Framework.Code;
using BattleScene.Framework.GameObjects;
using BattleScene.Framework.InputActions;
using BattleScene.Framework.ViewModel;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace BattleScene.Framework.View
{
    public class GridView : MonoBehaviour, BattleSceneInputAction.IBattleSceneActions
    {
        [SerializeField] private int maxGridSize;
        private Window _window;
        private Table _grid;
        private ArrowRight _arrowRight;
        private ArrowUp _arrowUp;
        private ArrowDown _arrowDown;
        private MessageView _messageView;
        private PlayerView _playerView;
        private GridViewDto _dto;
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
            _grid = GetComponentInChildren<Table>();
            _grid.SetItem(maxGridSize);
            _arrowRight = GetComponentInChildren<ArrowRight>();
            _arrowUp = GetComponentInChildren<ArrowUp>();
            _arrowDown = GetComponentInChildren<ArrowDown>();
            // TODO: もっといいGetComponentの方法があるかも
            var root = transform.root;
            _messageView = root.GetComponentInChildren<MessageView>();
            _playerView = root.GetComponentInChildren<PlayerView>();
            _inputAction = new BattleSceneInputAction();
            _inputAction.BattleScene.AddCallbacks(this);

            var actionCount = Enum.GetValues(typeof(ActionCode)).Length;
            _rowStateDictionary = new Dictionary<ActionCode, RowState>(actionCount);
        }

        public void StartAnimationAsync(GridViewDto dto)
        {
            enabled = true;
            _window.enabled = true;
            _inputAction.Enable();

            _dto = dto;
            if (!_rowStateDictionary.TryGetValue(dto.ActionCode, out var gridState))
            {
                gridState = new RowState(
                    maxTableSize: maxGridSize,
                    itemCount: dto.RowDtoList.Count);
                _rowStateDictionary.Add(dto.ActionCode, gridState);
            }

            _grid.enabled = true;

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
            _arrowUp.enabled = gridState.IsHiddenUpper;
            _arrowDown.enabled = gridState.IsHiddenLower;

            _messageView.enabled = true;
            _messageView.StartAnimation(new MessageViewModel(
                message: dto.RowDtoList[gridState.SelectedIndex].RowDescription,
                noWait: true));

            _playerView.enabled = true;
            _playerView.StartAnimation(new PlayerViewModel(
                playerImagePath: dto.RowDtoList[gridState.SelectedIndex].PlayerImagePath));
            _playerView.StartPlayerSlideView();
        }

        public void StopAnimation()
        {
            _window.enabled = false;
            _grid.enabled = false;
            _arrowRight.enabled = false;
            _arrowUp.enabled = false;
            _arrowDown.enabled = false;
            _inputAction.Disable();
            enabled = false;
        }

        private void MoveArrow(Vector2 vector2)
        {
            if (vector2.y == 0) return;

            if (vector2.y > 0)
                _rowStateDictionary[_dto.ActionCode].Up();
            else
                _rowStateDictionary[_dto.ActionCode].Down();
            StartAnimationAsync(_dto);
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var row = _rowStateDictionary[_dto.ActionCode].SelectedIndex;
                _selectRowAction.OnSelect(row);
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
    }
}