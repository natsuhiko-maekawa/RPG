using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Runner;

namespace BattleScene.UseCase.Service
{
    public class SelectSkillService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly CharactersDomainService _characters;
        private readonly SkillService _skill;
        private readonly ISkillSelectorRepository _skillSelectorRepository;

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