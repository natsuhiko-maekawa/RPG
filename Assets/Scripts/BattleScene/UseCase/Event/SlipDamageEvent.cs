using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.CharacterVibesView.OutputBoundary;
using BattleScene.UseCase.View.CharacterVibesView.OutputDataFactory;
using BattleScene.UseCase.View.DigitView.OutputBoundary;
using BattleScene.UseCase.View.DigitView.OutputDataFactory;
using BattleScene.UseCase.View.HitPointBarView.OutputBoundary;
using BattleScene.UseCase.View.HitPointBarView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using static BattleScene.UseCase.Event.Runner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCase.Event
{
    internal class SlipDamageEvent : IEvent, IWait
    {
        private readonly CharactersDomainService _characters;
        private readonly CharacterVibesOutputDataFactory _characterVibesOutputDataFactory;
        private readonly ICharacterVibesViewPresenter _characterVibesView;
        private readonly DamageDigitOutputDataFactory _damageDigitOutputDataFactory;
        private readonly IDigitViewPresenter _digitView;
        private readonly HitPointBarOutputDataFactory _hitPointBarOutputDataFactory;
        private readonly IHitPointBarViewPresenter _hitPointBarView;
        private readonly IHitPointRepository _hitPointRepository;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly ResultDomainService _result;
        private readonly IResultRepository _resultRepository;
        private readonly SlipDamageService _slipDamage;
        private readonly ITargetRepository _targetRepository;

        public SlipDamageEvent(
            CharactersDomainService characters,
            CharacterVibesOutputDataFactory characterVibesOutputDataFactory,
            ICharacterVibesViewPresenter characterVibesView,
            DamageDigitOutputDataFactory damageDigitOutputDataFactory,
            IDigitViewPresenter digitView,
            HitPointBarOutputDataFactory hitPointBarOutputDataFactory,
            IHitPointBarViewPresenter hitPointBarView,
            IHitPointRepository hitPointRepository,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView,
            ResultDomainService result,
            IResultRepository resultRepository,
            SlipDamageService slipDamage,
            ITargetRepository targetRepository)
        {
            _characters = characters;
            _characterVibesOutputDataFactory = characterVibesOutputDataFactory;
            _characterVibesView = characterVibesView;
            _damageDigitOutputDataFactory = damageDigitOutputDataFactory;
            _digitView = digitView;
            _hitPointBarOutputDataFactory = hitPointBarOutputDataFactory;
            _hitPointBarView = hitPointBarView;
            _hitPointRepository = hitPointRepository;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
            _result = result;
            _resultRepository = resultRepository;
            _slipDamage = slipDamage;
            _targetRepository = targetRepository;
        }

        public EventCode Run()
        {
            var result = _slipDamage.Damage();
            _resultRepository.Update(result);

            var playerId = _characters.GetPlayerId();
            var hitPoint = _hitPointRepository.Select(playerId);
            hitPoint.Reduce(_result.LastDamage().GetTotal());
            _hitPointRepository.Update(hitPoint);

            var target = _targetRepository.Select(playerId);
            target.Set(playerId);
            _targetRepository.Update(target);

            StartView();

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.LoopEndEvent;
        }

        private void StartView()
        {
            var digitOutputDataList = _damageDigitOutputDataFactory.Create();
            _digitView.Start(digitOutputDataList);
            _hitPointBarView.Start(_hitPointBarOutputDataFactory.Create());
            var messageOutputData = _messageOutputDataFactory.Create(SlipDamageMessage);
            _messageView.Start(messageOutputData);
            _characterVibesView.Start(_characterVibesOutputDataFactory.Create());
        }
    }
}