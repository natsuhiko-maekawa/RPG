using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.View.AilmentView.OutputBoundary;
using BattleScene.UseCase.View.AilmentView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using BattleScene.UseCase.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCase.View.PlayerImageView.OutputDataFactory;

namespace BattleScene.UseCase.Event
{
    public class AilmentSkillSuccessViewEvent : IEvent, IWait
    {
        private readonly AilmentOutputDataFactory _ailmentOutputDataFactory;
        private readonly AilmentPlayerImageOutputDataFactory _ailmentPlayerImageOutputDataFactory;
        private readonly ResultDomainService _result;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly ICharacterRepository _characterRepository;
        private readonly IAilmentViewPresenter _ailmentView;
        private readonly IMessageViewPresenter _messageView;
        private readonly IPlayerImageViewPresenter _playerImageView;
        
        public EventCode Run()
        {
            var ailmentSkillResult = _result.Last<AilmentSkillResultValueObject>();
            
            var ailmentOutputData = _ailmentOutputDataFactory.Create(ailmentSkillResult.TargetIdList);
            _ailmentView.Start(ailmentOutputData);
            var ailmentsMessageOutputData = _messageOutputDataFactory.Create(MessageCode.AilmentsMessage);
            _messageView.Start(ailmentsMessageOutputData);
            
            // プレイヤーが状態異常にかかった時、プレイヤーの立ち絵を更新する
            if (!_characterRepository.Select(ailmentSkillResult.ActorId).IsPlayer()) 
                return EventCode.WaitEvent;
            
            var playerImage = _ailmentPlayerImageOutputDataFactory.Create(ailmentSkillResult);
            _playerImageView.Start(playerImage);
            return EventCode.WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.SwitchSkillEvent;
        }
    }
}