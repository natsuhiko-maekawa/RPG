using System;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Views.GameObjects
{
    public class StatusBarMeter : MonoBehaviour
    {
        private Image _meter;

        private void Awake()
        {
            _meter = GetComponent<Image>();
        }

        public void Set(int currentPoint, int maxPoint)
        {
            var rate = Mathf.Min(currentPoint / (float)maxPoint, 1.0f);
            _meter.rectTransform.localScale = new Vector3(rate, 1.0f, 1.0f);
        }
    }
}