using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Presenter;
using static BattleScene.InterfaceAdapter.Presenter.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.PresenterFacade
{
    public class DamageOutputPresenterFacade
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly AttackCountViewPresenter _attackCountView;
        private readonly DamageViewPresenter _damageView;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly VibrationViewPresenter _vibrationView;

        public DamageOutputPresenterFacade(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            AttackCountViewPresenter attackCountView,
            DamageViewPresenter damageView,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView,
            VibrationViewPresenter vibrationView)
        {
            _characterRepository = characterRepository;
            _attackCountView = attackCountView;
            _damageView = damageView;
            _messageView = messageView;
            _playerImageView = playerImageView;
            _vibrationView = vibrationView;
        }

        public void Output(BattleEventEntity damageEvent)
        {
            var isActorPlayer = _characterRepository.Get(damageEvent.ActorId)!.IsPlayer;
            if (isActorPlayer)
            {
                _attackCountView.Start();
            }

            if (damageEvent.ActualTargetIdList.Any(x => _characterRepository.Get(x).IsPlayer))
            {
                var (playerImageCode, animationMode) = damageEvent.AttackList
                    .Where(x => _characterRepository.Get(x.TargetId).IsPlayer)
                    .All(x => !x.IsHit)
                    ? (PlayerImageCode.Avoidance, Slide)
                    : (PlayerImageCode.Damaged, Vibe);
                _playerImageView.StartAnimation(playerImageCode, animationMode);
            }

            var messageCode = GetMessageCode(damageEvent);
            _messageView.StartAnimation(messageCode);
            _damageView.StartAnimation();
            _vibrationView.StartAnimation();
        }

        private MessageCode GetMessageCode(BattleEventEntity damageEvent)
        {
            if (damageEvent.AttackList
                .All(x => !x.IsHit)) return MessageCode.AvoidMessage;
            if (DamagesOneself(damageEvent)) return MessageCode.DamageOneselfMessage;
            return damageEvent.AttackList
                .Any(x => x.AttacksWeakPoint)
                ? MessageCode.WeakPointMessage
                : MessageCode.DamageMessage;
        }

        private bool DamagesOneself(BattleEventEntity damageEvent)
        {
            var value = damageEvent.AttackList
                .Any(x => x.TargetId == damageEvent.ActorId);
            return value;
        }
    }
}