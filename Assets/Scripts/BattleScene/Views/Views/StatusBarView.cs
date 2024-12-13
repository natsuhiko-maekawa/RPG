using BattleScene.Views.GameObjects;
using BattleScene.Views.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Views.Views
{
    public class StatusBarView : MonoBehaviour
    {
        private Image _barFrontImage;
        private Text _barText;
        private StatusBarMeter _statusBarMeter;
        private StatusBarText _statusBarText;

        private void Awake()
        {
            _statusBarMeter = GetComponentInChildren<StatusBarMeter>();
            _statusBarText = GetComponentInChildren<StatusBarText>();
        }

        public void StartAnimation(StatusBarViewModel model)
        {
            _statusBarText.Set(model.CurrentPoint, model.MaxPoint);
            _statusBarMeter.Set(model.CurrentPoint, model.MaxPoint);
        }
    }
}