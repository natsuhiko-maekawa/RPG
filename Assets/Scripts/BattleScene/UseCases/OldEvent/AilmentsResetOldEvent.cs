using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.AilmentView.OutputBoundary;
using BattleScene.UseCases.View.AilmentView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using BattleScene.UseCases.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCases.View.PlayerImageView.OutputData;
using static BattleScene.UseCases.Constant;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class AilmentsResetOldEvent : IOldEvent, IWait
    {
        private readonly AilmentOutputDataFactory _ailmentOutputDataFactory;
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;
        private readonly IAilmentViewPresenter _ailmentView;
        private readonly CharactersDomainService _characters;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IPlayerImageViewPresenter _playerImageView;

        public AilmentsResetOldEvent(
            AilmentOutputDataFactory ailmentOutputDataFactory,
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository,
            IAilmentViewPresenter ailmentView,
            CharactersDomainService characters,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView,
            OrderedItemsDomainService orderedItems,
            IPlayerImageViewPresenter playerImageView)
        {
            _ailmentOutputDataFactory = ailmentOutputDataFactory;
            _ailmentRepository = ailmentRepository;
            _ailmentView = ailmentView;
            _characters = characters;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
            _orderedItems = orderedItems;
            _playerImageView = playerImageView;
        }

        public EventCode Run()
        {
            var playerId = _characters.GetPlayerId();
            if (_orderedItems.FirstAilmentCode() == AilmentCode.Confusion) { }
                // _skillRepository.Update(_skillCreator.Create(playerId, SkillCode.Attack));

            _ailmentRepository.Delete((playerId, _orderedItems.FirstAilmentCode()));
            StartView();

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return LoopEndEvent;
        }

        private void StartView()
        {
            var ailmentOutputData = _ailmentOutputDataFactory.Create(_characters.GetPlayerId());
            _ailmentView.Start(ailmentOutputData);
            var messageOutputData = _messageOutputDataFactory.Create(RecoverAilmentMessage);
            _messageView.Start(messageOutputData);
            _playerImageView.Start(new PlayerImageOutputData(DefaultImage));
        }
    }
}