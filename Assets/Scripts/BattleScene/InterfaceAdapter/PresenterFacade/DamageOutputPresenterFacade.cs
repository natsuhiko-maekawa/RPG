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

        public void Output(BattleEventValueObject damage)
        {
            var isActorPlayer = _characterRepository.Get(damage.ActorId)!.IsPlayer;
            if (isActorPlayer)
            {
                _attackCountView.Start();
            }

            if (damage.ActualTargetIdList.Any(x => _characterRepository.Get(x).IsPlayer))
            {
                var (playerImageCode, animationMode) = damage.AttackList
                    .Where(x => _characterRepository.Get(x.TargetId).IsPlayer)
                    .All(x => !x.IsHit)
                    ? (PlayerImageCode.Avoidance, Slide)
                    : (PlayerImageCode.Damaged, Vibe);
                _playerImageView.StartAnimation(playerImageCode, animationMode);
            }

            var messageCode = GetMessageCode(damage);
            _messageView.StartAnimation(messageCode);
            _damageView.StartAnimation();
            _vibrationView.StartAnimation();
        }

        private MessageCode GetMessageCode(BattleEventValueObject damage)
        {
            if (damage.IsAvoid) return MessageCode.AvoidMessage;
            if (DamagesOneself(damage)) return MessageCode.DamageOneselfMessage;
            return damage.AttacksWeakPoint
                ? MessageCode.WeakPointMessage
                : MessageCode.DamageMessage;
        }

        private bool DamagesOneself(BattleEventValueObject damage)
        {
            var value = damage.AttackList
                .Any(x => Equals(x.TargetId, damage.ActorId));
            return value;
        }
    }
}