﻿using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.InterfaceAdapter.DataAccess;
using BattleScene.InterfaceAdapter.DataAccess.Dto;
using BattleScene.InterfaceAdapter.IView;
using BattleScene.UseCases.View.EnemyView.OutputBoundary;
using BattleScene.UseCases.View.EnemyView.OutputData;

namespace BattleScene.InterfaceAdapter.Presenter.EnemyView
{
    internal class EnemyViewPresenter : IEnemyViewPresenter
    {
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRepository<EnemyEntity, CharacterId> _enemyRepository;
        private readonly IResource<EnemyViewInfoDto, CharacterTypeCode> _enemyViewInfoResource;
        private readonly IEnemiesView _enemiesView;

        public EnemyViewPresenter(
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            IRepository<EnemyEntity, CharacterId> enemyRepository,
            IResource<EnemyViewInfoDto, CharacterTypeCode> enemyViewInfoResource,
            IEnemiesView enemiesView)
        {
            _characterRepository = characterRepository;
            _enemyRepository = enemyRepository;
            _enemyViewInfoResource = enemyViewInfoResource;
            _enemiesView = enemiesView;
        }

        public void Start(EnemyOutputData enemyOutputData)
        {
            var enemyDto = enemyOutputData.EnemyCharacterIdList
                .Select(x =>
                {
                    var characterTypeId = _characterRepository.Select(x).Property.CharacterTypeCode;
                    return new EnemyDto(
                        EnemyNumber: _enemyRepository.Select(x).EnemyNumber,
                        EnemyImagePath: _enemyViewInfoResource.Get(characterTypeId).EnemyImagePath);
                })
                .ToImmutableList();
            var enemyViewDto = new EnemyViewDto(
                EnemyCount: enemyOutputData.EnemyCharacterIdList.Count,
                EnemyDtoList: enemyDto);
            
            _enemiesView.StartEnemyView(enemyViewDto);
        }
    }
}