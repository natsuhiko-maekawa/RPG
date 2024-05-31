﻿using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.View.FrameView.OutputBoundary;
using BattleScene.UseCase.View.FrameView.OutputData;

namespace BattleScene.UseCase.View.FrameView.OutputDataFactory
{
    public class TargetFrameOutputDataFactory
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IEnemyRepository _enemyRepository;
        private readonly IFrameViewPresenter _frameView;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ITargetRepository _targetRepository;

        public ImmutableList<FrameOutputData> Create()
        {
            var playerId = _orderedItems.FirstCharacterId();
            return _targetRepository.Select(playerId).TargetIdList
                .Select(x =>
                {
                    var isPlayer = _characterRepository.Select(x).IsPlayer();
                    return isPlayer
                        ? new FrameOutputData(
                            true,
                            default,
                            FrameType.Target)
                        : new FrameOutputData(
                            false,
                            _enemyRepository.Select(x).EnemyNumber,
                            FrameType.Target);
                })
                .ToImmutableList();
        }
    }
}