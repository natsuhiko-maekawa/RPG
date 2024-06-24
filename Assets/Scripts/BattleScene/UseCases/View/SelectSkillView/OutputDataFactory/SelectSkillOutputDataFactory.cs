using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Event.Runner;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View.SelectSkillView.OutputData;

namespace BattleScene.UseCases.View.SelectSkillView.OutputDataFactory
{
    public class SelectSkillOutputDataFactory
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly CharactersDomainService _characters;
        private readonly SkillService _skill;
        private readonly SkillCreatorService _skillCreatorService;
        private readonly ISkillSelectorRepository _skillSelectorRepository;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;

        public SelectSkillOutputDataFactory(
            ICharacterRepository characterRepository,
            CharactersDomainService characters,
            SkillService skill,
            SkillCreatorService skillCreatorService,
            ISkillSelectorRepository skillSelectorRepository,
            ISkillViewInfoFactory skillViewInfoFactory)
        {
            _characterRepository = characterRepository;
            _characters = characters;
            _skill = skill;
            _skillCreatorService = skillCreatorService;
            _skillSelectorRepository = skillSelectorRepository;
            _skillViewInfoFactory = skillViewInfoFactory;
        }

        public SelectSkillOutputData Create(EventCode eventCode)
        {
            var skillSelector = _skillSelectorRepository.Select(new SkillSelectorId(eventCode));
            var selector = skillSelector.GetSelector();
            var skillCodeList = _characterRepository.Select(_characters.GetPlayerId()).GetSkills();
            var skillInfoList = skillSelector.GetSkillList(skillCodeList)
                .Select(x => new SkillInfo(
                    _skillViewInfoFactory.Create(x).SkillName,
                    _skillCreatorService.Create(_characters.GetPlayerId(), x).AbstractSkill.GetTechnicalPoint(),
                    _skill.Available(_characters.GetPlayerId(), x)
                ))
                .ToImmutableList();
            return new SelectSkillOutputData(
                selector.Selection,
                selector.ListStart,
                selector.UpperLimit,
                skillInfoList);
        }
    }
}