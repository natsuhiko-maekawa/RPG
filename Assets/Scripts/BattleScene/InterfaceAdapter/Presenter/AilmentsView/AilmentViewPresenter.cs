using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.IRepository;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.InterfaceAdapter.Service;
using BattleScene.UseCases.View.AilmentView.OutputBoundary;
using BattleScene.UseCases.View.AilmentView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.AilmentsView
{
    internal class AilmentViewPresenter : IAilmentViewPresenter
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IEnemiesView _enemiesView;
        private readonly IEnemyRepository _enemyRepository;
        private readonly IPlayerStatusView _playerStatusView;
        private readonly ToAilmentNumberService _toAilmentNumber;

        public AilmentViewPresenter(
            ICharacterRepository characterRepository,
            IEnemiesView enemiesView,
            IEnemyRepository enemyRepository,
            IPlayerStatusView playerStatusView,
            ToAilmentNumberService toAilmentNumber)
        {
            _characterRepository = characterRepository;
            _enemiesView = enemiesView;
            _enemyRepository = enemyRepository;
            _playerStatusView = playerStatusView;
            _toAilmentNumber = toAilmentNumber;
        }

        public void Start(AilmentOutputData ailmentOutputData)
        {
            var ailmentNumberList = ToAilmentNumberList(ailmentOutputData);
            var character = _characterRepository.Select(ailmentOutputData.CharacterId);
            if (character.IsPlayer())
            {
                var playerAilmentsViewDtoList = new PlayerAilmentsViewDto(ailmentNumberList);
                _playerStatusView.StartPlayerAilmentsView(playerAilmentsViewDtoList);
            }
            else
            {
                var enemyNumber = _enemyRepository.Select(ailmentOutputData.CharacterId).EnemyNumber;
                var enemyAilmentsViewDto = new EnemyAilmentsViewDto(enemyNumber, ailmentNumberList);
                _enemiesView.StartEnemyAilmentsView(enemyAilmentsViewDto);
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
                .Select(_toAilmentNumber.Ailment)
                .Concat(ailmentOutputData.SlipDamageCodeList
                    .Select(_toAilmentNumber.SlipDamage))
                .ToImmutableList();
        }
    }
}