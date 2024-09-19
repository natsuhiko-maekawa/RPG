using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Framework.View;
using BattleScene.Framework.ViewModel;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.View.AilmentView.OutputBoundary;
using BattleScene.UseCases.View.AilmentView.OutputData;

namespace BattleScene.InterfaceAdapter.ObsoletePresenter
{
    [Obsolete]
    internal class AilmentViewPresenter : IAilmentViewPresenter
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly EnemiesView _enemiesView;
        private readonly PlayerStatusView _playerStatusView;

        public AilmentViewPresenter(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            EnemiesView enemiesView,
            PlayerStatusView playerStatusView)
        {
            _characterRepository = characterRepository;
            _enemiesView = enemiesView;
            _playerStatusView = playerStatusView;
        }

        public void Start(AilmentOutputData ailmentOutputData)
        {
            var ailmentNumberList = ToAilmentNumberList(ailmentOutputData);
            var character = _characterRepository.Select(ailmentOutputData.CharacterId);
            if (character.IsPlayer)
            {
                var playerAilmentsViewDtoList = new PlayerAilmentsViewDto(ailmentNumberList);
                // _playerStatusView.StartPlayerAilmentsView(playerAilmentsViewDtoList);
            }
            else
            {
                var enemyPosition = _characterRepository.Select(ailmentOutputData.CharacterId).Position;
                var enemyAilmentsViewDto = new EnemyAilmentsViewDto(enemyPosition, ailmentNumberList);
                _enemiesView[enemyPosition].StartAilmentAnimationAsync(enemyAilmentsViewDto);
            }
        }

        public void Start(IList<AilmentOutputData> ailmentOutputDataList)
        {
            foreach (var ailmentOutputData in ailmentOutputDataList)
                Start(ailmentOutputData);
        }

        private ImmutableList<int> ToAilmentNumberList(AilmentOutputData ailmentOutputData)
        {
            return ailmentOutputData.AilmentCodeList
                .Select(AilmentIdConverter.Ailment)
                .Concat(ailmentOutputData.SlipDamageCodeList
                    .Select(AilmentIdConverter.SlipDamage))
                .ToImmutableList();
        }
    }
}