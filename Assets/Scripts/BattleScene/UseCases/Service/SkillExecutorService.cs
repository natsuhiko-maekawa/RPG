using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

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
            if (!_orderedItems.First().TryGetCharacterId(out var actorId)) return;
            if (!_characterRepository.Select(actorId).IsPlayer) return;
            
            var skill = _skillFactory.Create(skillCode);
            var technicalPoint = skill.SkillCommon.TechnicalPoint;
            _characterRepository.Select(actorId).CurrentTechnicalPoint -= technicalPoint;
        }
    }
}