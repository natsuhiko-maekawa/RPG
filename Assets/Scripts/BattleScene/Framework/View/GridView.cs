﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter.GridView;
using BattleScene.InterfaceAdapter.Presenter.MessageView;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.View
{
    public class GridView : MonoBehaviour, IGridView
    {
        [SerializeField] private int slotHeight = 46;
        [SerializeField] private int rightArrowX = -147;
        [SerializeField] private int rightArrowY = 69;
        [SerializeField] private Text text;
        [SerializeField] private Image rightArrow;
        [SerializeField] private Image window;
        [SerializeField] private MessageView messageView;
        private readonly List<Text> _textList = new();
        private Image _rightArrow;
        private Image _window;
        private int _selectedRow;

        private void Awake()
        {
            _window = Instantiate(window, transform);
            _window.enabled = false;
            _rightArrow = Instantiate(rightArrow, transform);
            _rightArrow.enabled = false;
        }

        public async Task StartAnimationAsync(GridViewDto dto)
        {
            _window.enabled = true;
            _rightArrow.enabled = true;
            _rightArrow.rectTransform.localPosition 
                = new Vector3(rightArrowX, -_selectedRow * slotHeight + rightArrowY, 0);
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

            var message = dto.RowList[_selectedRow].RowDescription;
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
    }
}