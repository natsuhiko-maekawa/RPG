using UnityEngine;

namespace BattleScene.Framework.GameObjects
{
    public class Row : MonoBehaviour
    {
        private RowName _rowName;
        private TechnicalPoint _technicalPoint;

        public void Awake()
        {
            _rowName = GetComponentInChildren<RowName>();
            _technicalPoint = GetComponentInChildren<TechnicalPoint>();
        }

        public void SetName(string rowName) => _rowName.Set(rowName);
        public void ShowName() => _rowName.Show();
        public void SetTechnicalPoint(int technicalPoint) => _technicalPoint.Set(technicalPoint);
        public void ShowTechnicalPoint() => _technicalPoint.Show();

        public void Hide()
        {
            _rowName.Hide();
            _technicalPoint.Hide();
        }
    }
}