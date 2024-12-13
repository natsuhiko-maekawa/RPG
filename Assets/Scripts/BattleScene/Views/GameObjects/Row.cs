using UnityEngine;

namespace BattleScene.Views.GameObjects
{
    public class Row : MonoBehaviour
    {
        private RowName _rowName;
        private TechnicalPoint _technicalPoint;

        public void Awake()
        {
            _rowName = GetComponentInChildren<RowName>();
            _technicalPoint = GetComponentInChildren<TechnicalPoint>();
            enabled = false;
        }

        public void SetName(string rowName) => _rowName.Set(rowName);
        public void ShowName() => _rowName.enabled = true;
        public void HighlightName() => _rowName.Highlight();
        public void UnhighlightName() => _rowName.Unhighlight();
        public void SetTechnicalPoint(int technicalPoint) => _technicalPoint.Set(technicalPoint);
        public void ShowTechnicalPoint() => _technicalPoint.enabled = true;

        private void OnDisable()
        {
            _rowName.enabled = false;
            _technicalPoint.enabled = false;
        }
    }
}