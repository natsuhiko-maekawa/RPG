using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.SelectSkillView.OutputData;

namespace BattleScene.UseCase.View.SelectSkillView.OutputDataFactory
{
    public class SelectSkillOutputDataFactory
    {
        private readonly CharactersDomainService _characters;
        private readonly SkillService _skill;
        private readonly ICharacterRepository _characterRepository;
        private readonly SkillCreatorService _skillCreatorService;
        private readonly ISkillSelectorRepository _skillSelectorRepository;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;
        
        public SelectSkillOutputData Create(EventCode eventCode)
        {
            var skillSelector = _skillSelectorRepository.Select(new SkillSelectorId(eventCode));
            var selector = skillSelector.GetSelector();
            var skillCodeList = _characterRepository.Select(_characters.GetPlayerId()).GetSkills();
            var skillInfoList = skillSelector.GetSkillList(skillCodeList)
                .Select(x => new SkillInfo(
                    Name: _skillViewInfoFactory.Create(x).SkillName,
                    Tp: _skillCreatorService.Create(_characters.GetPlayerId(), x).AbstractSkill.GetTechnicalPoint(),
                    Disabled: _skill.Available(_characters.GetPlayerId(), x)
                ))
                .ToImmutableList();
            return new SelectSkillOutputData(
                Selection: selector.Selection, 
                ListStart: selector.ListStart,
                UpperLimit: selector.UpperLimit,
                SkillList: skillInfoList);
        }
    }
}