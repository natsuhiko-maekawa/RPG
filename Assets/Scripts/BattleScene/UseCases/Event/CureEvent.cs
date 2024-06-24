using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Event.Interface;
using BattleScene.UseCases.Event.Runner;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View.DigitView.OutputBoundary;
using BattleScene.UseCases.View.DigitView.OutputDataFactory;
using BattleScene.UseCases.View.HitPointBarView.OutputBoundary;
using BattleScene.UseCases.View.HitPointBarView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using static BattleScene.UseCases.Event.Runner.EventCode;

namespace BattleScene.UseCases.Event
{
    internal class CureEvent : IEvent, IWait
    {
        private readonly CureDigitOutputDataFactory _cureDigitOutputDataFactory;
        private readonly CureSkillService _cureSkill;
        private readonly IDigitViewPresenter _digitViewPresenter;
        private readonly HitPointBarOutputDataFactory _hitPointBarOutputDataFactory;
        private readonly IHitPointBarViewPresenter _hitPointBarViewPresenter;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageViewPresenter;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultDomainService _result;
        private readonly IResultRepository _resultRepository;
        private readonly ISkillRepository _skillRepository;

        public CureEvent(
            CureDigitOutputDataFactory cureDigitOutputDataFactory,
            CureSkillService cureSkill,
            IDigitViewPresenter digitViewPresenter,
            HitPointBarOutputDataFactory hitPointBarOutputDataFactory,
            IHitPointBarViewPresenter hitPointBarViewPresenter,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageViewPresenter,
            OrderedItemsDomainService orderedItems,
            ResultDomainService result,
            IResultRepository resultRepository,
            ISkillRepository skillRepository)
        {
            _cureDigitOutputDataFactory = cureDigitOutputDataFactory;
            _cureSkill = cureSkill;
            _digitViewPresenter = digitViewPresenter;
            _hitPointBarOutputDataFactory = hitPointBarOutputDataFactory;
            _hitPointBarViewPresenter = hitPointBarViewPresenter;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageViewPresenter = messageViewPresenter;
            _orderedItems = orderedItems;
            _result = result;
            _resultRepository = resultRepository;
            _skillRepository = skillRepository;
        }

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