using System;
using BattleScene.DataAccess;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.Service;
using R3;

namespace BattleScene.InterfaceAdapter.ReactivePresenter
{
    internal class AilmentViewReactivePresenter : IReactive<AilmentEntity>
    {
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly ToIndexService _toIndex;
        private readonly EnemiesView _enemiesView;
        private readonly PlayerStatusView _playerAilmentsView;

        public AilmentViewReactivePresenter(
            ICollection<CharacterEntity, CharacterId> characterCollection,
            ToIndexService toIndex,
            EnemiesView enemiesView,
            PlayerStatusView playerAilmentsView)
        {
            _characterCollection = characterCollection;
            _toIndex = toIndex;
            _enemiesView = enemiesView;
            _playerAilmentsView = playerAilmentsView;
        }

        public void Observe(AilmentEntity ailment)
        {
            var character = _characterCollection.Get(ailment.CharacterId);
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
            var position = _characterCollection.Get(characterId).Position;
            var ailmentId = _toIndex.FromAilment(ailmentCode);
            var dto = new AilmentViewModel(ailmentId, effects);
            _enemiesView[position].StartAilmentAnimationAsync(dto);
        }
    }
}