using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter.SelectActionView;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.UserInterface.View
{
    public class SelectActionView : MonoBehaviour, ISelectActionView
    {
        private const int SlotHeight = 46;
        [SerializeField] private Text text;
        [SerializeField] private Image window;
        [SerializeField] private Image rightArrow;
        private readonly List<Text> _textList = new();
        private Image _rightArrow;
        private Image _window;

        private void Awake()
        {
            _window = Instantiate(window, transform);
            _window.enabled = false;
            _rightArrow = Instantiate(rightArrow, transform);
            _rightArrow.enabled = false;
        }

        public Task StartAnimation(SelectActionViewDto dto)
        {
            _window.enabled = true;
            _rightArrow.enabled = true;
            _rightArrow.rectTransform.localPosition = new Vector3(-147, -dto.HighlightRow * SlotHeight + 69, 0);
            SetText(dto.ViewLength);
            for (var i = 0; i < dto.TextList.Count; i++)
            {
                _textList[i].text = dto.TextList[i];
                Color color;
                if (dto.DisabledRowList.Contains(i))
                    color = Color.gray;
                else if (i == dto.HighlightRow)
                    color = Color.red;
                else
                    color = Color.white;
                _textList[i].color = color;
                _textList[i].enabled = true;
            }

            return Task.CompletedTask;
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
                vector3.y += -i * SlotHeight;
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