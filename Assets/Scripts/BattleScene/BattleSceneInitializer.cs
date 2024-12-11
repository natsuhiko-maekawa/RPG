using System;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Presenter;
using UnityEngine;
using VContainer;

namespace BattleScene
{
    public class BattleSceneInitializer : MonoBehaviour
    {
        private PlayerImageViewPresenter _playerImageView;

        [Inject]
        public void Construct(
            PlayerImageViewPresenter playerImageView)
        {
            _playerImageView = playerImageView;
        }

        private async void Awake()
        {
            await SetPlayerImage();
        }

        private async Task SetPlayerImage()
        {
            var playerImageCodeArray = Enum.GetValues(typeof(PlayerImageCode))
                .Cast<PlayerImageCode>()
                .Where(x => x != PlayerImageCode.NoImage)
                .ToArray();

            await _playerImageView.SetImage(playerImageCodeArray);
        }
    }
}