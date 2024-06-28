using BattleScene.Domain.Entity;
using BattleScene.Domain.IFactory;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCases.IPresenter;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class PlayerSkillViewPresenter : IPlayerSkillViewPresenter
    {
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;
        private readonly IPlayerView _playerView;
        
        public void Out(SkillEntity skill)
        {
            var playerImageCode = _skillViewInfoFactory.Create(skill.SkillCode).PlayerImageCode;
        }
    }
}