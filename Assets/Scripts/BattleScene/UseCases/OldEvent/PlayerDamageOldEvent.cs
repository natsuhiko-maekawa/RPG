using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess.ObsoleteIFactory;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View.AttackCountView.OutputBoundary;
using BattleScene.UseCases.View.AttackCountView.OutputDataFactory;
using BattleScene.UseCases.View.CharacterVibesView.OutputBoundary;
using BattleScene.UseCases.View.CharacterVibesView.OutputDataFactory;
using BattleScene.UseCases.View.DigitView.OutputBoundary;
using BattleScene.UseCases.View.DigitView.OutputDataFactory;
using BattleScene.UseCases.View.HitPointBarView.OutputBoundary;
using BattleScene.UseCases.View.HitPointBarView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using BattleScene.UseCases.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCases.View.PlayerImageView.OutputData;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class PlayerDamageOldEvent : IOldEvent, IWait
    {
        private readonly AttackCountOutputDataFactory _attackCountOutputDataFactory;
        private readonly IAttackCountViewPresenter _attackCountViewPresenter;
        private readonly ICharacterRepository _characterRepository;
        private readonly CharacterVibesOutputDataFactory _characterVibesOutputDataFactory;
        private readonly ICharacterVibesViewPresenter _characterVibesView;
        private readonly DamageDigitOutputDataFactory _damageDigitOutputDataFactory;
        private readonly DamageMessageOutputDataFactory _damageMessageOutputDataFactory;
        private readonly DamageGeneratorService _damageGenerator;
        private readonly IDigitViewPresenter _digitView;
        private readonly HitPointDomainService _hitPoint;
        private readonly HitPointBarOutputDataFactory _hitPointBarOutputDataFactory;
        private readonly IHitPointBarViewPresenter _hitPointBarView;
        private readonly IMessageViewPresenter _messageViewPresenter;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IPlayerImageViewPresenter _playerImageView;
        private readonly ResultDomainService _result;
        private readonly IResultRepository _resultRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;

        public PlayerDamageOldEvent(
            AttackCountOutputDataFactory attackCountOutputDataFactory,
            IAttackCountViewPresenter attackCountViewPresenter,
            ICharacterRepository characterRepository,
            CharacterVibesOutputDataFactory characterVibesOutputDataFactory,
            ICharacterVibesViewPresenter characterVibesView,
            DamageDigitOutputDataFactory damageDigitOutputDataFactory,
            DamageMessageOutputDataFactory damageMessageOutputDataFactory,
            DamageGeneratorService damageGenerator,
            IDigitViewPresenter digitView,
            HitPointDomainService hitPoint,
            HitPointBarOutputDataFactory hitPointBarOutputDataFactory,
            IHitPointBarViewPresenter hitPointBarView,
            IMessageViewPresenter messageViewPresenter,
            OrderedItemsDomainService orderedItems,
            IPlayerImageViewPresenter playerImageView,
            ResultDomainService result,
            IResultRepository resultRepository,
            ISkillRepository skillRepository,
            ISkillViewInfoFactory skillViewInfoFactory)
        {
            _attackCountOutputDataFactory = attackCountOutputDataFactory;
            _attackCountViewPresenter = attackCountViewPresenter;
            _characterRepository = characterRepository;
            _characterVibesOutputDataFactory = characterVibesOutputDataFactory;
            _characterVibesView = characterVibesView;
            _damageDigitOutputDataFactory = damageDigitOutputDataFactory;
            _damageMessageOutputDataFactory = damageMessageOutputDataFactory;
            _damageGenerator = damageGenerator;
            _digitView = digitView;
            _hitPoint = hitPoint;
            _hitPointBarOutputDataFactory = hitPointBarOutputDataFactory;
            _hitPointBarView = hitPointBarView;
            _messageViewPresenter = messageViewPresenter;
            _orderedItems = orderedItems;
            _playerImageView = playerImageView;
            _result = result;
            _resultRepository = resultRepository;
            _skillRepository = skillRepository;
            _skillViewInfoFactory = skillViewInfoFactory;
        }

        public EventCode Run()
        {
            var characterId = _orderedItems.FirstCharacterId();
            var skill = _skillRepository.Select(characterId);
            var result = _damageGenerator.Execute(skill);
            _resultRepository.Update(result);

            var isAvoid = !_result.Last<DamageValueObject>().Success();
            if (!_characterRepository.Select(characterId).IsPlayer())
                _playerImageView.Start(new PlayerImageOutputData(
                    isAvoid
                        ? PlayerImageCode.Avoidance
                        : _skillViewInfoFactory.Create(_skillRepository.Select(characterId).SkillCode)
                            .PlayerImageCode));

            if (isAvoid)
            {
                var characterVibesOutputData = _characterVibesOutputDataFactory.Create();
                _characterVibesView.Start(characterVibesOutputData);
            }

            var digitOutputData = _damageDigitOutputDataFactory.Create();
            _digitView.Start(digitOutputData);
            var hitPointBarOutputData = _hitPointBarOutputDataFactory.Create();
            _hitPointBarView.Start(hitPointBarOutputData);
            var messageOutputData = _damageMessageOutputDataFactory.Create();
            _messageViewPresenter.Start(messageOutputData);
            var attackCountOutputData = _attackCountOutputDataFactory.Create();
            _attackCountViewPresenter.Start(attackCountOutputData);

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return _result.Last<DamageValueObject>().Success()
                ? GetIndex()
                : GetIndexWhenAvoid();
        }

        private EventCode GetIndex()
        {
            if (_hitPoint.AnyIsDead()) return EventCode.PlayerBeatEnemyEvent;
            return EventCode.SwitchSkillEvent;
        }

        private EventCode GetIndexWhenAvoid()
        {
            if (_hitPoint.AnyIsDead()) return EventCode.PlayerBeatEnemyEvent;
            return EventCode.SwitchSkillEvent;
        }
    }
}