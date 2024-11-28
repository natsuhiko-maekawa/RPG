using Cysharp.Text;
using TMPro;
using UnityEngine;

namespace BattleScene.Framework.GameObjects
{
    public class TechnicalPoint : MonoBehaviour
    {
        private TextMeshProUGUI _technicalPoint;

        private void Awake()
        {
            _technicalPoint = GetComponent<TextMeshProUGUI>();
            enabled = false;
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

        private void OnDisable()
        {
            _technicalPoint.enabled = false;
        }
    }
}