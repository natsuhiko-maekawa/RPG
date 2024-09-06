﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter.GridView;
using BattleScene.InterfaceAdapter.Presenter.MessageView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace BattleScene.Framework.View
{
    public class GridView : MonoBehaviour, IGridView
    {
        [SerializeField] private int slotHeight = 46;
        [SerializeField] private int rightArrowX = -147;
        [SerializeField] private int rightArrowY = 69;
        [SerializeField] private int maxGridSize = 4;
        [SerializeField] private Text text;
        [SerializeField] private Image rightArrow;
        [SerializeField] private Image upArrow;
        [SerializeField] private Image downArrow;
        [SerializeField] private Image window;
        [SerializeField] private MessageView messageView;
        [SerializeField] private InputAction moveAction;
        [SerializeField] private InputAction selectAction;
        private readonly List<Text> _textList = new();
        private Image _rightArrow;
        private Image _upArrow;
        private Image _downArrow;
        private Image _window;
        private GridViewDto _dto;
        private GridState _gridState;
        private int _id;
        public int SlotHeight => slotHeight;
        public int Id => _id;
        
        private void Awake()
        {
            // TODO: PrefabをアタッチしてGetComponentに置き換える
            _window = Instantiate(window, transform);
            _window.enabled = false;
            _rightArrow = Instantiate(rightArrow, transform);
            _rightArrow.enabled = false;
            _upArrow = Instantiate(upArrow, transform);
            _upArrow.enabled = false;
            _downArrow = Instantiate(downArrow, transform);
            _downArrow.enabled = false;
            SetMoveAction(MoveArrow);
        }

        public async Task StartAnimationAsync(GridViewDto dto)
        {
            _window.enabled = true;
            _rightArrow.enabled = true;

            _dto = dto;
            _gridState ??= new GridState(
                maxGridSize: maxGridSize,
                itemCount: dto.RowList.Count);
            
            _rightArrow.rectTransform.localPosition 
                = new Vector3(rightArrowX, -_gridState.SelectedRow * slotHeight + rightArrowY, 0);
            _upArrow.enabled = _gridState.IsHiddenUpper;
            _downArrow.enabled = _gridState.IsHiddenLower;
            
            SetText(dto.RowList.Count);
            foreach (var (row, index) in dto.RowList.Select((x, i) => (x, i)))
            {
                _textList[index].text = row.RowName;
                Color color;
                if (!row.Enabled)
                    color = Color.gray;
                else
                    color = Color.white;
                _textList[index].color = color;
                _textList[index].enabled = true;
            }

            var message = dto.RowList[_gridState.SelectedRow].RowDescription;
            var messageViewDto = new MessageViewDto(message, NoWait:true);
            await messageView.StartAnimation(messageViewDto);
        }

        public void StopAnimation()
        {
            _window.enabled = false;
            _rightArrow.enabled = false;
            foreach (var textObject in _textList)
                textObject.enabled = false;
        }

        private void SetText(int textCount)
        {
            if (textCount > _textList.Count)
            {
                AddText(textCount - _textList.Count);
                return;
            }

            if (textCount < _textList.Count) RemoveText(_textList.Count - textCount);
        }

        private void AddText(int textCount)
        {
            for (var i = 0; i < textCount; ++i)
            {
                var t = Instantiate(text, transform);
                _textList.Add(t);
                var vector3 = _textList[i].transform.localPosition;
                vector3.y += -i * slotHeight;
                _textList[i].transform.localPosition = vector3;
                _textList[i].enabled = false;
            }
        }

        private void RemoveText(int textCount)
        {
            for (var i = _textList.Count - 1; i > textCount; --i)
            {
                Destroy(_textList[i]);
                _textList.RemoveAt(i);
            }
        }

        private async Task MoveArrow(Vector2 vector2)
        {
            if (vector2.y == 0) return;
            
            if (vector2.y > 0)
                _gridState.Up();
            else
                _gridState.Down();
            var selectedRow = _gridState?.SelectedRow ?? 0;
            _id = _dto?.RowList[selectedRow].RowId ?? 0;
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