using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.CharacterVibesView.OutputBoundary;
using BattleScene.UseCases.View.CharacterVibesView.OutputDataFactory;
using BattleScene.UseCases.View.DigitView.OutputBoundary;
using BattleScene.UseCases.View.DigitView.OutputDataFactory;
using BattleScene.UseCases.View.HitPointBarView.OutputBoundary;
using BattleScene.UseCases.View.HitPointBarView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class SlipDamageOldEvent : IOldEvent, IWait
    {
        private readonly CharactersDomainService _characters;
        private readonly CharacterVibesOutputDataFactory _characterVibesOutputDataFactory;
        private readonly ICharacterVibesViewPresenter _characterVibesView;
        private readonly DamageDigitOutputDataFactory _damageDigitOutputDataFactory;
        private readonly IDigitViewPresenter _digitView;
        private readonly HitPointBarOutputDataFactory _hitPointBarOutputDataFactory;
        private readonly IHitPointBarViewPresenter _hitPointBarView;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly ResultDomainService _result;

        public SlipDamageOldEvent(
            CharactersDomainService characters,
            CharacterVibesOutputDataFactory characterVibesOutputDataFactory,
            ICharacterVibesViewPresenter characterVibesView,
            DamageDigitOutputDataFactory damageDigitOutputDataFactory,
            IDigitViewPresenter digitView,
            HitPointBarOutputDataFactory hitPointBarOutputDataFactory,
            IHitPointBarViewPresenter hitPointBarView,
            IRepository<HitPointAggregate, CharacterId> hitPointRepository,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView,
            ResultDomainService result)
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
        }

        public EventCode Run()
        {
            // var result = _slipDamage.Damage();
            // _resultRepository.Update(result);

            var playerId = _characters.GetPlayerId();
            var hitPoint = _hitPointRepository.Select(playerId);
            hitPoint.Reduce(_result.LastDamage().GetTotal());
            _hitPointRepository.Update(hitPoint);

            // var target = _targetRepository.Select(playerId);
            // target.Set(playerId);
            // _targetRepository.Update(target);

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