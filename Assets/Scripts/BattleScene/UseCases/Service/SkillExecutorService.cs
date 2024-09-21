using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using UnityEngine;

namespace BattleScene.UseCases.Service
{
    public class SkillExecutorService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;
        private readonly OrderedItemsDomainService _orderedItems;

        public SkillExecutorService(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IFactory<SkillValueObject, SkillCode> skillFactory,
            OrderedItemsDomainService orderedItems)
        {
            _characterRepository = characterRepository;
            _skillFactory = skillFactory;
            _orderedItems = orderedItems;
        }

        public void Execute(SkillCode skillCode)
        {
            var skill = _skillFactory.Create(skillCode);
            var technicalPoint = skill.SkillCommon.TechnicalPoint;
            _orderedItems.First().TryGetCharacterId(out var actorId);
            Debug.Assert(actorId != null);
            var actor = _characterRepository.Select(actorId);
            if (!actor.IsPlayer) return;

            _characterRepository.Select(actorId).CurrentTechnicalPoint -= technicalPoint;
        }
    }
}