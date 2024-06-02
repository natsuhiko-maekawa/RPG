using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter.SelectSkillView;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.View
{
    public class SelectSkillView : MonoBehaviour, ISelectSkillView
    {
        private const int SlotHeight = 46;
        [SerializeField] private Text text;
        [SerializeField] private TextMeshProUGUI tp;
        [SerializeField] private Image window;
        [SerializeField] private Image upArrow;
        [SerializeField] private Image downArrow;
        [SerializeField] private Image rightArrow;
        private readonly Color _gray = new(0.5215687f, 0.5215687f, 0.5215687f);
        private readonly Color _lightBlue = new(0.5215687f, 0.5215687f, 0.9803922f);
        private readonly Color _lightCyan = new(0.5215687f, 0.9215686f, 0.9803922f);
        private readonly Color _lightRed = new(0.9803922f, 0.5215687f, 0.5215687f);
        private readonly Color _lightYellow = new(0.9803922f, 0.9215686f, 0.5215687f);
        private readonly List<Text> _textList = new();
        private readonly List<TextMeshProUGUI> _tpList = new();
        private readonly Color _white = new(0.9803922f, 0.9803922f, 0.9803922f);
        private Image _downArrow;
        private Image _rightArrow;
        private Image _upArrow;
        private Image _window;

        private void Awake()
        {
            _window = Instantiate(window, transform);
            _window.enabled = false;
            _rightArrow = Instantiate(rightArrow, transform);
            _rightArrow.enabled = false;
            _upArrow = Instantiate(upArrow, transform);
            _upArrow.enabled = false;
            _downArrow = Instantiate(downArrow, transform);
            _downArrow.enabled = false;
        }

        public Task StartAnimation(SelectSkillViewDto dto)
        {
            _window.enabled = true;
            _rightArrow.enabled = true;
            _rightArrow.rectTransform.localPosition = new Vector3(-147, -dto.HighlightRow * SlotHeight + 69, 0);
            _upArrow.enabled = dto.ViewUpArrow;
            _downArrow.enabled = dto.ViewDownArrow;
            SetText(dto.SkillDtoList.Count);
            foreach (var (skill, index) in dto.SkillDtoList.Select((x, i) => (x, i)))
            {
                _textList[index].text = skill.Name;
                if (skill.Disabled)
                    _textList[index].color = Color.gray;
                else if (index == dto.HighlightRow)
                    _textList[index].color = Color.red;
                else
                    _textList[index].color = Color.white;
                _textList[index].enabled = true;

                _tpList[index].text = skill.Tp.ToString();
                if (skill.Disabled)
                    _tpList[index].colorGradient = new VertexGradient(_white, _white, _gray, _gray);
                else if (index == dto.HighlightRow)
                    _tpList[index].colorGradient = new VertexGradient(_lightYellow, _lightYellow, _lightRed, _lightRed);
                else
                    _tpList[index].colorGradient = new VertexGradient(_lightCyan, _lightCyan, _lightBlue, _lightBlue);
                _tpList[index].enabled = true;
            }

            return Task.CompletedTask;
        }

        public void StopAnimation()
        {
            _window.enabled = false;
            _rightArrow.enabled = false;
            _upArrow.enabled = false;
            _downArrow.enabled = false;
            foreach (var t in _textList) t.enabled = false;
            foreach (var t in _tpList) t.enabled = false;
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
                _textList.Add(Instantiate(text, transform));
                _textList[i].transform.localPosition += new Vector3(0, -i * SlotHeight, 0);
                _textList[i].enabled = false;

                _tpList.Add(Instantiate(tp, transform));
                _tpList[i].transform.localPosition += new Vector3(0, -i * SlotHeight, 0);
                _tpList[i].enabled = false;
            }
        }

        private void RemoveText(int textCount)
        {
            for (var i = _textList.Count - 1; i > textCount; --i)
            {
                Destroy(_textList[i]);
                _textList.RemoveAt(i);
                Destroy(_tpList[i]);
                _tpList.RemoveAt(i);
            }
        }
    }
}