using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;
using BattleScene.InterfaceAdapter.DataAccess.IResource;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Presenter.MessageView;
using BattleScene.InterfaceAdapter.Presenter.PlayerView;
using BattleScene.InterfaceAdapter.Presenter.StatusBarView;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.IPresenter;

namespace BattleScene.InterfaceAdapter.Presenter
{
    /// <summary>
    /// プレイヤーがスキルを発動したときに画面に出力する情報を変換してViewに渡す。<br/>
    /// 立ち絵、TPバー、メッセージの表示を更新する。
    /// </summary>
    public class PlayerSkillViewPresenter : IPlayerSkillViewPresenter
    {
        private readonly CharactersDomainService _characters;
        private readonly MessageCodeConverterService _messageCodeConverter;
        private readonly IFactory<PlayerViewInfoValueObject, PlayerImageCode> _playerViewInfoFactory;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;
        private readonly IRepository<TechnicalPointAggregate, CharacterId> _technicalPointRepository;
        private readonly IMessageView _messageView;
        private readonly IPlayerView _playerView;
        
        public void Out(SkillEntity skill)
        {
            var playerImageCode = _skillViewInfoFactory.Create(skill.SkillCode).PlayerImageCode;
            var playerImagePath = _playerViewInfoFactory.Create(playerImageCode).PlayerImagePath;
            var playerViewDto = new PlayerViewDto(playerImagePath);
            _playerView.StartAnimation(playerViewDto);

            var playerId = _characters.GetPlayerId();
            var currentTechnicalPoint = _technicalPointRepository.Select(playerId).GetCurrent();
            var maxTechnicalPoint = _technicalPointRepository.Select(playerId).GetMax();
            var technicalPointBarViewDto = new TechnicalPointBarViewDto(
                MaxPoint: maxTechnicalPoint,
                CurrentPoint: currentTechnicalPoint);
            _playerView.StartTechnicalPointBarView(technicalPointBarViewDto);
            
            var messageCode = skill.AbstractSkill.GetType().ToString().Split(".").Last() == "AttackSkill"
                ? MessageCode.AttackMessage
                : MessageCode.SkillMessage;
            var message = _messageCodeConverter.ToMessage(messageCode);
            var messageViewDto = new MessageViewDto(
                Message: message);
            _messageView.StartAnimation(messageViewDto);
        }
    }
}