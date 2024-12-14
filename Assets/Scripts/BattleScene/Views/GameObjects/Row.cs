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
        public void ShowName()
        {
            enabled = true;
            _rowName.enabled = true;
        }

        public void Highlight()
        {
            _rowName.Highlight();
            _technicalPoint.Highlight();
        }

        public void Unhighlight()
        {
            _rowName.Unhighlight();
            _technicalPoint.Unhighlight();
        }

        public void Inactive()
        {
            _rowName.Inactive();
            _technicalPoint.Inactive();
        }

        public void SetTechnicalPoint(int technicalPoint) => _technicalPoint.Set(technicalPoint);
        public void ShowTechnicalPoint()
        {
            enabled = true;
            _technicalPoint.enabled = true;
        }

        private void OnDisable()
        {
            _rowName.enabled = false;
            _technicalPoint.enabled = false;
        }
    }
}