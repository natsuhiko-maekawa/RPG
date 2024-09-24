using System.Collections.Generic;
using System.Threading.Tasks;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.Presenter;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class DamageOutputFacade
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly AttackCountViewPresenter _attackCountView;
        private readonly DamageViewPresenter _damageView;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;
        private readonly VibrationViewPresenter _vibrationView;

        public DamageOutputFacade(
            IRepository<CharacterEntity, CharacterId> characterRepository, 
            OrderedItemsDomainService orderedItems,
            AttackCountViewPresenter attackCountView,
            DamageViewPresenter damageView,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView,
            VibrationViewPresenter vibrationView)
        {
            _characterRepository = characterRepository;
            _orderedItems = orderedItems;
            _attackCountView = attackCountView;
            _damageView = damageView;
            _messageView = messageView;
            _playerImageView = playerImageView;
            _vibrationView = vibrationView;
        }

        public async Task Output()
        {
            var animationList = new List<Task>();
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var isActorPlayer = _characterRepository.Select(actorId).IsPlayer;
            if (isActorPlayer)
            {
                var attackCountAnimation = _attackCountView.Start();
                animationList.Add(attackCountAnimation);
            }
            else
            {
                var playerImageAnimation = _playerImageView.StartAnimationAsync(PlayerImageCode.Damaged);
                animationList.Add(playerImageAnimation);
            }
            
            var damageMessageAnimation = _messageView.StartDamageMessageAnimationAsync();
            var damageAnimation = _damageView.StartAnimationAsync();
            var vibrationAnimation = _vibrationView.StartAnimationAsync();
            animationList.Add(damageMessageAnimation);
            animationList.Add(damageAnimation);
            animationList.Add(vibrationAnimation);
            await Task.WhenAll(animationList);
        }
    }
}