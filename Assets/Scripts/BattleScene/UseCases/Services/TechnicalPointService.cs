﻿using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IServices;

namespace BattleScene.UseCases.Services
{
    public class TechnicalPointService : ITechnicalPointService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public TechnicalPointService(
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public int Get()
        {
            var player = _characterRepository.Get()
                .Single(x => x.IsPlayer);
            var technicalPoint = player.CurrentTechnicalPoint;
            return technicalPoint;
        }

        public void Reduce(SkillValueObject skill)
        {
            var player = _characterRepository.Get()
                .Single(x => x.IsPlayer);
            var technicalPoint = skill.Common.TechnicalPoint;
            player.CurrentTechnicalPoint -= technicalPoint;
        }

        public void Restore(IReadOnlyList<BattleEventEntity> restoreEventList)
        {
            foreach (var restoreEvent in restoreEventList)
            {
                var player = _characterRepository.Get()
                    .Single(x => x.IsPlayer);
                var technicalPoint = restoreEvent.TechnicalPoint;
                player.CurrentTechnicalPoint += technicalPoint;
            }
        }
    }
}