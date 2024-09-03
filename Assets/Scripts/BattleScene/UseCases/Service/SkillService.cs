using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.UseCases.Service
{
    public class SkillService
    {
        private readonly BodyPartDomainService _bodyPart;
        private readonly CharactersDomainService _characters;
        private readonly ICharacterRepository _characterRepository;
        private readonly SkillCreatorService _skillCreatorService;
        private readonly IRepository<TechnicalPointEntity, CharacterId> _technicalPointRepository;

        public SkillService(
            BodyPartDomainService bodyPart,
            CharactersDomainService characters,
            ICharacterRepository characterRepository,
            SkillCreatorService skillCreatorService,
            IRepository<TechnicalPointEntity, CharacterId> technicalPointRepository)
        {
            _bodyPart = bodyPart;
            _characters = characters;
            _characterRepository = characterRepository;
            _skillCreatorService = skillCreatorService;
            _technicalPointRepository = technicalPointRepository;
        }

        public bool Available(CharacterId characterId, SkillCode skillCode)
        {
            var skill = _skillCreatorService.Create(characterId, skillCode);
            return _characterRepository.Select(characterId).IsPlayer()
                ? PlayerAvailable(characterId, skill)
                : EnemyAvailable(characterId, skill);
        }

        private bool PlayerAvailable(CharacterId characterId, SkillEntity skill)
        {
            return TechnicalPointAvailable(skill)
                   && BodyPartAvailable(characterId, skill);
        }

        private bool EnemyAvailable(CharacterId characterId, SkillEntity skill)
        {
            return BodyPartAvailable(characterId, skill);
        }

        private bool TechnicalPointAvailable(SkillEntity skill)
        {
            var playerId = _characters.GetPlayerId();
            var technicalPointAggregate = _technicalPointRepository.Select(playerId);
            return skill.Skill.TechnicalPoint <= technicalPointAggregate.GetCurrent();
        }

        private bool BodyPartAvailable(CharacterId characterId, SkillEntity skill)
        {
            return _bodyPart.IsAvailable(characterId, skill.Skill.DependencyList);
        }
    }
}