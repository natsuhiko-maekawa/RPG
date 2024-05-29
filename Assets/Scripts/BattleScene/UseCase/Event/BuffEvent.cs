using System;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.Skill.Interface;
using BattleScene.UseCase.View.BuffView.OutputBoundary;
using BattleScene.UseCase.View.BuffView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using static BattleScene.UseCase.EventRunner.EventCode;

namespace BattleScene.UseCase.Event
{
    internal class BuffEvent : IEvent, IWait
    {
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly CharactersDomainService _characters;
        private readonly ResultDomainService _result;
        private readonly ISkillRepository _skillRepository;
        private readonly BuffOutputDataFactory _buffOutputDataFactory;
        private readonly IBuffViewInfoFactory _buffViewInfoFactory;
        private readonly IBuffViewPresenter _buffView;
        private readonly IMessageViewPresenter _messageView;
        
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
            return  EventCode.LoopEndEvent;
        }
    }
}