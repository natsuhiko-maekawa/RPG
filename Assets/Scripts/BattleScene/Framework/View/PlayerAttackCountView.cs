using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.Interface;
using BattleScene.InterfaceAdapter.Presenter.PlayerAttackCountView;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Framework.View
{
    public class PlayerAttackCountView : MonoBehaviour, IPlayerAttackCountView
    {
        [SerializeField] private Image barFrontImage;
        private Image _image;

        private void Awake()
        {
            _image = Instantiate(barFrontImage, transform);
        }

        public Task StartAnimation(PlayerAttackCountViewDto dto)
        {
            _image.rectTransform.localScale = new Vector3(dto.Rate, 1.0f, 1.0f);
            return Task.CompletedTask;
        }
    }
}