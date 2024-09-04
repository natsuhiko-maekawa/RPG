using System.Threading.Tasks;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using UnityEngine;

namespace BattleScene.Framework.View
{
    public class TargetView : MonoBehaviour, ITargetView
    {
        private EnemiesView _enemiesView;
        private PlayerView _playerView;

        private void Start()
        {
            _enemiesView = GetComponentInChildren<EnemiesView>();
            _playerView = GetComponentInChildren<PlayerView>();
        }

        public Task StartAnimation(TargetViewDto dto)
        {
            var frameViewDto = new FrameViewDto(Color.red);
            foreach (var character in dto.CharacterDtoList)
            {
                if (character.IsPlayer)
                {
                    _playerView.StartFrameView(frameViewDto);
                    continue;
                }
                
                _enemiesView[character.EnemyIndex].StartFrameView(frameViewDto);
            }
            
            return Task.CompletedTask;
        }
    }
}