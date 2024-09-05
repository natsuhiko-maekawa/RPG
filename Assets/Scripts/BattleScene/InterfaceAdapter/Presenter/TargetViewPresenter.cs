using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCases.IPresenter;

namespace BattleScene.InterfaceAdapter.Presenter
{
    public class TargetViewPresenter : ITargetViewPresenter
    {
        private readonly PlayerDomainService _player;
        private readonly IRepository<EnemyEntity, CharacterId> _enemyRepository;
        private readonly ITargetView _targetView;

        public TargetViewPresenter(
            PlayerDomainService player, 
            IRepository<EnemyEntity, CharacterId> enemyRepository, 
            ITargetView targetView)
        {
            _player = player;
            _enemyRepository = enemyRepository;
            _targetView = targetView;
        }

        public void Start(IList<CharacterId> targetIdList)
        {
            var characterDtoList = targetIdList
                .Select(x => Equals(x, _player.GetId())
                    ? CharacterDto.CreatePlayer()
                    : new CharacterDto(_enemyRepository.Select(x).EnemyNumber))
                .ToImmutableList();
            var targetViewDto = new TargetViewDto(characterDtoList);
            _targetView.StartAnimation(targetViewDto);
        }

        public void Stop()
        {
            _targetView.StopAnimation();
        }
    }
}