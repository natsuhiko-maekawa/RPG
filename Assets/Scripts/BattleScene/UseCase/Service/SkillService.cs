using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.UseCase.Service
{
    public class SkillService
    {
        private readonly BodyPartDomainService _bodyPart;
        private readonly ICharacterRepository _characterRepository;
        private readonly SkillCreatorService _skillCreatorService;
        private readonly ITechnicalPointRepository _technicalPointRepository;

        public SkillService(
            BodyPartDomainService bodyPart,
            ICharacterRepository characterRepository,
            SkillCreatorService skillCreatorService,
            ITechnicalPointRepository technicalPointRepository)
        {
            _bodyPart = bodyPart;
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
            var technicalPointAggregate = _technicalPointRepository.Select();
            return skill.AbstractSkill.GetTechnicalPoint() <= technicalPointAggregate.GetCurrent();
        }

        private bool BodyPartAvailable(CharacterId characterId, SkillEntity skill)
        {
            return _bodyPart.IsAvailable(characterId, skill.AbstractSkill.GetDependencyList());
        }
    }
}