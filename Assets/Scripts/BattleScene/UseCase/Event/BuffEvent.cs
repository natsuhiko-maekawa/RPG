using System;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Skill.Interface;
using BattleScene.UseCase.View.BuffView.OutputBoundary;
using BattleScene.UseCase.View.BuffView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using static BattleScene.UseCase.Event.Runner.EventCode;

namespace BattleScene.UseCase.Event
{
    internal class BuffEvent : IEvent, IWait
    {
        private readonly BuffOutputDataFactory _buffOutputDataFactory;
        private readonly IBuffViewPresenter _buffView;
        private readonly IBuffViewInfoFactory _buffViewInfoFactory;
        private readonly CharactersDomainService _characters;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultDomainService _result;
        private readonly ISkillRepository _skillRepository;

        public BuffEvent(
            IBuffViewPresenter buffView,
            IBuffViewInfoFactory buffViewInfoFactory,
            CharactersDomainService characters,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView,
            OrderedItemsDomainService orderedItems,
            ResultDomainService result,
            ISkillRepository skillRepository)
        {
            _buffView = buffView;
            _buffViewInfoFactory = buffViewInfoFactory;
            _characters = characters;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
            _orderedItems = orderedItems;
            _result = result;
            _skillRepository = skillRepository;
        }

        public EventCode Run()
        {
            if (_skillRepository.Select(_orderedItems.FirstCharacterId()).AbstractSkill is not IBuffSkill)
                throw new InvalidCastException();

            var buffSkillResult = _result.Last<BuffSkillResultValueObject>();

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