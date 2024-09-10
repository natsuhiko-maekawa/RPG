namespace BattleScene.InterfaceAdapter.Presenter
{
    /// <summary>
    /// プレイヤーがスキルを発動したときに画面に出力する情報を変換してViewに渡す。<br/>
    /// 立ち絵、TPバー、メッセージの表示を更新する。
    /// </summary>
    public class PlayerSkillViewPresenter
    {
        // public void Out(SkillEntity skill)
        // {
        //     var playerImageCode = _skillViewInfoFactory.Get(skill.SkillCode).PlayerImageCode;
        //     var playerImagePath = _playerViewInfoResource.Get(playerImageCode).PlayerImagePath;
        //     var playerViewDto = new PlayerViewDto(playerImagePath);
        //     _playerView.StartAnimation(playerViewDto);
        //     
        //     var playerId = _characters.GetPlayerId();
        //     var currentTechnicalPoint = _technicalPointRepository.Select(playerId).GetCurrent();
        //     var maxTechnicalPoint = _technicalPointRepository.Select(playerId).GetMax();
        //     var technicalPointBarViewDto = new TechnicalPointBarViewDto(
        //         MaxPoint: maxTechnicalPoint,
        //         CurrentPoint: currentTechnicalPoint);
        //     _playerView.StartTechnicalPointBarView(technicalPointBarViewDto);
        //     
        //     // TODO: SkillMessageViewPresenterに移動すること
        //     var messageCode = skill.Skill.GetType().ToString().Split(".").Last() == "AttackSkill"
        //         ? MessageCode.AttackMessage
        //         : MessageCode.SkillMessage;
        //     var message = _messageCodeConverter.ToMessage(messageCode);
        //     var messageViewDto = new MessageViewDto(
        //         Message: message);
        //     _messageView.StartAnimation(messageViewDto);
        // }
    }
}