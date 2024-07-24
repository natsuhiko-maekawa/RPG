using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.UseCases.OldEvent.Runner;

namespace BattleScene.UseCases.Service
{
    public class SelectSkillService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly CharactersDomainService _characters;
        private readonly SkillService _skill;
        private readonly ISkillSelectorRepository _skillSelectorRepository;

        public SelectSkillService(
            ICharacterRepository characterRepository,
            CharactersDomainService characters,
            SkillService skill,
            ISkillSelectorRepository skillSelectorRepository)
        {
            _characterRepository = characterRepository;
            _characters = characters;
            _skill = skill;
            _skillSelectorRepository = skillSelectorRepository;
        }

        public bool CanUpdate(EventCode eventCode)
        {
            var playerId = _characters.GetPlayerId();
            var skillSelector = _skillSelectorRepository.Select(new SkillSelectorId(eventCode));
            var skillCode = skillSelector.GetSkill(_characterRepository.Select(playerId).GetSkills());
            return _skill.Available(playerId, skillCode);
        }

        public SkillCode GetSkillCode(EventCode eventCode)
        {
            var playerId = _characters.GetPlayerId();
            var skillSelector = _skillSelectorRepository.Select(new SkillSelectorId(eventCode));
            return skillSelector.GetSkill(_characterRepository.Select(playerId).GetSkills());
        }
    }
}