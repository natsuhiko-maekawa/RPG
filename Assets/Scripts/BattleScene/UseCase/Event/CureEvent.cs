using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.DigitView.OutputBoundary;
using BattleScene.UseCase.View.DigitView.OutputDataFactory;
using BattleScene.UseCase.View.HitPointBarView.OutputBoundary;
using BattleScene.UseCase.View.HitPointBarView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using static BattleScene.UseCase.EventRunner.EventCode;

namespace BattleScene.UseCase.Event
{
    internal class CureEvent : IEvent, IWait
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly MessageOutputDataFactory _messageGenerator;
        private readonly OrderedItemsDomainService _order;
        private readonly CureSkillService _cureSkill;
        private readonly ResultDomainService _result;
        private readonly HitPointBarOutputDataFactory _hitPointBarOutputDataFactory;
        private readonly CureDigitOutputDataFactory _cureDigitOutputDataFactory;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IResultRepository _resultRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IDigitViewPresenter _digitViewPresenter;
        private readonly IHitPointBarViewPresenter _hitPointBarViewPresenter;
        private readonly IMessageViewPresenter _messageViewPresenter;
        
        public EventCode Run()
        {
            var characterId = _orderedItems.FirstCharacterId();
            var skill = _skillRepository.Select(characterId);
            var result = _cureSkill.Execute(skill);
            _resultRepository.Update(result);

            if (_result.Last<CureSkillResultValueObject>().Success())
            {
                var cureDigitOutputData = _cureDigitOutputDataFactory.Create();
                _digitViewPresenter.Start(cureDigitOutputData);
                var hitPointBarOutputData = _hitPointBarOutputDataFactory.Create();
                _hitPointBarViewPresenter.Start(hitPointBarOutputData);
            }

            var messageOutputData = _messageOutputDataFactory.Create(MessageCode.RestoreHitPointMessage);
            _messageViewPresenter.Start(messageOutputData);

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.SwitchSkillEvent;
        }
    }
}