using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.Service;
using R3;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class AilmentViewPresenter : DataAccess.IObserver<AilmentEntity>
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly EnemiesView _enemiesView;
        private readonly PlayerStatusView _playerAilmentsView;

        public AilmentViewPresenter(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            EnemiesView enemiesView,
            PlayerStatusView playerAilmentsView)
        {
            _characterRepository = characterRepository;
            _enemiesView = enemiesView;
            _playerAilmentsView = playerAilmentsView;
        }

        public void Observe(AilmentEntity ailment)
        {
            var character = _characterRepository.Select(ailment.CharacterId);
            Action<bool> startAilmentViewAction = character.IsPlayer
                ? x => StartPlayerAilmentView(ailment.AilmentCode, x)
                : x => StartEnemyAilmentView(ailment.CharacterId, ailment.AilmentCode, x);
            ailment.ReactiveEffects.Subscribe(startAilmentViewAction);
        }

        private void StartPlayerAilmentView(AilmentCode ailmentCode, bool effects)
        {
            var ailmentId = AilmentIdConverter.Ailment(ailmentCode);
            var dto = new AilmentViewModel(ailmentId, effects);
            _playerAilmentsView.StartPlayerAilmentsView(dto);
        }

        private void StartEnemyAilmentView(CharacterId characterId, AilmentCode ailmentCode, bool effects)
        {
            var position = _characterRepository.Select(characterId).Position;
            var ailmentId = AilmentIdConverter.Ailment(ailmentCode);
            var dto = new AilmentViewModel(ailmentId, effects);
            _enemiesView[position].StartAilmentAnimationAsync(dto);
        }
    }
}