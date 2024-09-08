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
            Hide();
        }

        public void Show() => _technicalPoint.enabled = true;
        public void Hide() => _technicalPoint.enabled = false;
    }
}