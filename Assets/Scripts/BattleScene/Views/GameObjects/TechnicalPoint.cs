using Cysharp.Text;
using TMPro;
using UnityEngine;

namespace BattleScene.Views.GameObjects
{
    public class TechnicalPoint : MonoBehaviour
    {
        public VertexGradient defaultColor;
        public VertexGradient highlightColor;
        public VertexGradient inactiveColor;
        private readonly Color _gray = new(0.5215687f, 0.5215687f, 0.5215687f);
        private readonly Color _lightBlue = new(0.5215687f, 0.5215687f, 0.9803922f);
        private readonly Color _lightCyan = new(0.5215687f, 0.9215686f, 0.9803922f);
        private readonly Color _lightRed = new(0.9803922f, 0.5215687f, 0.5215687f);
        private readonly Color _lightYellow = new(0.9803922f, 0.9215686f, 0.5215687f);
        private readonly Color _white = new(0.9803922f, 0.9803922f, 0.9803922f);
        private TextMeshProUGUI _technicalPoint;

        private void Awake()
        {
            _technicalPoint = GetComponent<TextMeshProUGUI>();
            enabled = false;
        }

        private void Reset()
        {
            defaultColor = new VertexGradient(_lightCyan, _lightCyan, _lightBlue, _lightBlue);
            highlightColor = new VertexGradient(_lightYellow, _lightYellow, _lightRed, _lightRed);
            inactiveColor = new VertexGradient(_white, _white, _gray, _gray);
        }

        private void OnEnable()
        {
            _technicalPoint.enabled = true;
        }

        public void Set(int technicalPoint)
        {
            using (var stringBuilder = ZString.CreateStringBuilder())
            {
                stringBuilder.Append(technicalPoint);
                _technicalPoint.SetText(stringBuilder);
            }
        }

        public void Highlight() => _technicalPoint.colorGradient = highlightColor;
        public void Unhighlight() => _technicalPoint.colorGradient = defaultColor;
        public void Inactive() => _technicalPoint.colorGradient = inactiveColor;

        private void OnDisable()
        {
            _technicalPoint.enabled = false;
        }
    }
}