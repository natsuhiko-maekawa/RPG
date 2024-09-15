using System.Threading.Tasks;
using BattleScene.Framework.GameObjects;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.View
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

        public Task StartAnimation(StatusBarViewDto dto)
        {
            _statusBarText.Set(dto.CurrentPoint, dto.MaxPoint);
            _statusBarMeter.Set(dto.CurrentPoint, dto.MaxPoint);
            
            return Task.CompletedTask;
        }
    }
}