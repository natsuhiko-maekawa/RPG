using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter.FrameView;
using BattleScene.UseCases.IPresenter;
using UnityEngine;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class TargetViewPresenter : ITargetViewPresenter
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IEnemiesView _enemiesView;
        private readonly IPlayerView _playerView;
        
        public void Out(TargetEntity target)
        {
            foreach (var targetId in target.TargetIdList)
            {
                var character = _characterRepository.Select(targetId);
                if (character.IsPlayer())
                    OutPlayer(targetId);
            }
        }

        private void OutPlayer(CharacterId characterId)
        {
            var dto = new FrameViewDto(Color.white);
            _playerView.StartFrameView(dto);
        }
    }
}