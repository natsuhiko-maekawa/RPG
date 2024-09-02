using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.BuffView.OutputBoundary;
using BattleScene.UseCases.View.BuffView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class BuffOldEvent : IOldEvent, IWait
    {
        private readonly BuffOutputDataFactory _buffOutputDataFactory;
        private readonly IBuffViewPresenter _buffView;
        private readonly IBuffViewInfoFactory _buffViewInfoFactory;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultDomainService _result;
        private readonly ISkillRepository _skillRepository;

        public BuffOldEvent(
            BuffOutputDataFactory buffOutputDataFactory,
            IBuffViewPresenter buffView,
            IBuffViewInfoFactory buffViewInfoFactory,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView,
            OrderedItemsDomainService orderedItems,
            ResultDomainService result,
            ISkillRepository skillRepository)
        {
            _buffOutputDataFactory = buffOutputDataFactory;
            _buffView = buffView;
            _buffViewInfoFactory = buffViewInfoFactory;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
            _orderedItems = orderedItems;
            _result = result;
            _skillRepository = skillRepository;
        }

        public EventCode Run()
        {
            // if (_skillRepository.Select(_orderedItems.FirstCharacterId()).Skill is not IBuffSkill)
            //     throw new InvalidCastException();

            var buffSkillResult = _result.Last<BuffValueObject>();

            var buffOutputData = _buffOutputDataFactory.Create(buffSkillResult.TargetIdList);
            _buffView.Start(buffOutputData);

            var buffViewInfo = _buffViewInfoFactory.Create(buffSkillResult.BuffCode);
            var messageOutputData = _messageOutputDataFactory.Create(buffViewInfo.MessageCode);
            _messageView.Start(messageOutputData);

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.LoopEndEvent;
        }
    }
}