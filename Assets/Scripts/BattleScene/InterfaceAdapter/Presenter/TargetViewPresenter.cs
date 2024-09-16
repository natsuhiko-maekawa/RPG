using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Framework.View;
using BattleScene.InterfaceAdapter.Presenter.Dto;
using BattleScene.UseCases.IPresenter;

namespace BattleScene.InterfaceAdapter.Presenter.ViewPresenter
{
    public class TargetViewPresenter : ITargetViewPresenter
    {
        private readonly PlayerDomainService _player;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly TargetView _targetView;

        public TargetViewPresenter(
            PlayerDomainService player, 
            IRepository<CharacterEntity, CharacterId> characterRepository, 
            TargetView targetView)
        {
            _player = player;
            _characterRepository = characterRepository;
            _targetView = targetView;
        }

        public void Start(IList<CharacterId> targetIdList)
        {
            var characterDtoList = targetIdList
                .Select(x => Equals(x, _player.GetId())
                    ? CharacterDto.CreatePlayer()
                    : new CharacterDto(_characterRepository.Select(x).Position))
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