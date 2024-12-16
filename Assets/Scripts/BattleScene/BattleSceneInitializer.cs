#nullable disable

using System;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.Presenters;
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

        private void Awake()
        {
            var _ = SetPlayerImage();
        }

        private async Task SetPlayerImage()
        {
            var playerImageCodeArray = Enum.GetValues(typeof(PlayerImageCode))
                .Cast<PlayerImageCode>()
                .ToArray();

            await _playerImageView.SetImage(playerImageCodeArray);
        }
    }
}