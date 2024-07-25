using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.AilmentView.OutputBoundary;
using BattleScene.UseCases.View.AilmentView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using BattleScene.UseCases.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCases.View.PlayerImageView.OutputDataFactory;

namespace BattleScene.UseCases.OldEvent
{
    internal class AilmentSkillSuccessViewOldEvent : IOldEvent, IWait
    {
        private readonly AilmentOutputDataFactory _ailmentOutputDataFactory;
        private readonly AilmentPlayerImageOutputDataFactory _ailmentPlayerImageOutputDataFactory;
        private readonly IAilmentViewPresenter _ailmentView;
        private readonly ICharacterRepository _characterRepository;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly IPlayerImageViewPresenter _playerImageView;
        private readonly ResultDomainService _result;

        public AilmentSkillSuccessViewOldEvent(
            AilmentOutputDataFactory ailmentOutputDataFactory,
            AilmentPlayerImageOutputDataFactory ailmentPlayerImageOutputDataFactory,
            IAilmentViewPresenter ailmentView,
            ICharacterRepository characterRepository,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView,
            IPlayerImageViewPresenter playerImageView,
            ResultDomainService result)
        {
            _ailmentOutputDataFactory = ailmentOutputDataFactory;
            _ailmentPlayerImageOutputDataFactory = ailmentPlayerImageOutputDataFactory;
            _ailmentView = ailmentView;
            _characterRepository = characterRepository;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
            _playerImageView = playerImageView;
            _result = result;
        }

        public EventCode Run()
        {
            var ailmentSkillResult = _result.Last<AilmentResultValueObject>();

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