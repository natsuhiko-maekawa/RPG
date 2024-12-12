using System;
using BattleScene.DataAccess;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Framework.ViewModels;
using BattleScene.Framework.Views;
using BattleScene.InterfaceAdapter.Services;
using R3;

namespace BattleScene.InterfaceAdapter.ReactivePresenters
{
    public class AilmentViewReactivePresenter : IReactive<AilmentEntity>
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly ToIndexService _toIndex;
        private readonly EnemiesView _enemiesView;
        private readonly PlayerStatusView _playerAilmentsView;

        public AilmentViewReactivePresenter(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            ToIndexService toIndex,
            EnemiesView enemiesView,
            PlayerStatusView playerAilmentsView)
        {
            _characterRepository = characterRepository;
            _toIndex = toIndex;
            _enemiesView = enemiesView;
            _playerAilmentsView = playerAilmentsView;
        }

        public void Observe(AilmentEntity ailment)
        {
            var character = _characterRepository.Get(ailment.CharacterId);
            Action<bool> startAilmentViewAction = character.IsPlayer
                ? x => StartPlayerAilmentView(ailment.AilmentCode, x)
                : x => StartEnemyAilmentView(ailment.CharacterId, ailment.AilmentCode, x);
            ailment.ReactiveEffects.Subscribe(startAilmentViewAction);
        }

        private void StartPlayerAilmentView(AilmentCode ailmentCode, bool effects)
        {
            var ailmentId = _toIndex.FromAilment(ailmentCode);
            var dto = new AilmentViewModel(ailmentId, effects);
            _playerAilmentsView.StartPlayerAilmentsView(dto);
        }

        private void StartEnemyAilmentView(CharacterId characterId, AilmentCode ailmentCode, bool effects)
        {
            var position = _characterRepository.Get(characterId).Position;
            var ailmentId = _toIndex.FromAilment(ailmentCode);
            var dto = new AilmentViewModel(ailmentId, effects);
            _enemiesView[position].StartAilmentAnimationAsync(dto);
        }
    }
}