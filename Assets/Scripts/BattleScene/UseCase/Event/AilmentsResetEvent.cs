using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.AilmentView.OutputBoundary;
using BattleScene.UseCase.View.AilmentView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using BattleScene.UseCase.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCase.View.PlayerImageView.OutputData;
using static BattleScene.UseCase.Constant;
using static BattleScene.UseCase.Event.Runner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCase.Event
{
    internal class AilmentsResetEvent : IEvent, IWait
    {
        private readonly AilmentOutputDataFactory _ailmentOutputDataFactory;
        private readonly IAilmentRepository _ailmentRepository;
        private readonly IAilmentViewPresenter _ailmentView;
        private readonly CharactersDomainService _characters;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IOrderRepository _orderRepository;
        private readonly IPlayerImageViewPresenter _playerImageView;
        private readonly SkillCreatorService _skillCreator;
        private readonly ISkillRepository _skillRepository;

        public EventCode Run()
        {
            var playerId = _characters.GetPlayerId();
            if (_orderedItems.FirstAilmentCode() == AilmentCode.Confusion)
                _skillRepository.Update(_skillCreator.Create(playerId, SkillCode.Attack));

            _ailmentRepository.Delete(playerId, _orderedItems.FirstAilmentCode());
            StartView();

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.LoopEndEvent;
        }

        private void StartView()
        {
            var ailmentOutputData = _ailmentOutputDataFactory.Create(_characters.GetPlayerId());
            _ailmentView.Start(ailmentOutputData);
            var messageOutputData = _messageOutputDataFactory.Create(RecoverAilmentsMessage);
            _messageView.Start(messageOutputData);
            _playerImageView.Start(new PlayerImageOutputData(DefaultImage));
        }
    }
}