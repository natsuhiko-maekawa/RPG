using System.Threading.Tasks;
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

        private void Awake()
        {
            _barFrontImage = Instantiate(barFrontImage, transform);
            _barText = Instantiate(barText, transform);
        }

        public Task StartAnimation(StatusBarViewDto dto)
        {
            _barText.text = dto.CurrentPoint + "/" + dto.MaxPoint;
            var hpRate = dto.MaxPoint < dto.CurrentPoint ? 1.0f : dto.CurrentPoint / (float)dto.MaxPoint;
            _barFrontImage.rectTransform.localScale = new Vector3(hpRate, 1.0f, 1.0f);
            return Task.CompletedTask;
        }
    }
}