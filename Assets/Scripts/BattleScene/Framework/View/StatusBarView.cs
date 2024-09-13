using System.Threading.Tasks;
using BattleScene.Framework.GameObjects;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.View
{
    public class StatusBarView : MonoBehaviour
    {
        [SerializeField] private Image barFrontImage;
        [SerializeField] private Text barText;
        private Image _barFrontImage;
        private Text _barText;
        private StatusBarMeter _statusBarMeter;
        private StatusBarText _statusBarText;

        private void Awake()
        {
            // _barFrontImage = Instantiate(barFrontImage, transform);
            // _barText = Instantiate(barText, transform);
            _statusBarMeter = GetComponentInChildren<StatusBarMeter>();
            _statusBarText = GetComponentInChildren<StatusBarText>();
        }

        public Task StartAnimation(StatusBarViewDto dto)
        {
            // _barText.text = dto.CurrentPoint + "/" + dto.MaxPoint;
            // var hpRate = dto.MaxPoint < dto.CurrentPoint ? 1.0f : dto.CurrentPoint / (float)dto.MaxPoint;
            // _barFrontImage.rectTransform.localScale = new Vector3(hpRate, 1.0f, 1.0f);
            
            _statusBarText.Set(dto.CurrentPoint, dto.MaxPoint);
            _statusBarMeter.Set(dto.CurrentPoint, dto.MaxPoint);
            
            return Task.CompletedTask;
        }
    }
}