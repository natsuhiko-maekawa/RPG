using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IFactory;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.IResource;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCases.IPresenter;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class PlayerSkillViewPresenter : IPlayerSkillViewPresenter
    {
        private readonly IResource<PlayerViewInfoValueObject, PlayerImageCode> _playerViewInfoResource;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;
        private readonly IPlayerView _playerView;
        
        public void Out(SkillEntity skill)
        {
            var playerImageCode = _skillViewInfoFactory.Create(skill.SkillCode).PlayerImageCode;
            var playerImagePath = _playerViewInfoResource.Select(playerImageCode);
        }
    }
}