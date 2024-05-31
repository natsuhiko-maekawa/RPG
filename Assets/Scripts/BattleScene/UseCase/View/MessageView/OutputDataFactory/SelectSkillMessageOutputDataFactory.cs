using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.View.MessageView.OutputData;

namespace BattleScene.UseCase.View.MessageView.OutputDataFactory
{
    public class SelectSkillMessageOutputDataFactory
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly CharactersDomainService _characters;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly ISkillSelectorRepository _skillSelectorRepository;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;

        public MessageOutputData Create(EventCode eventCode)
        {
            var skillSelector = _skillSelectorRepository.Select(new SkillSelectorId(eventCode));
            var skill = skillSelector.GetSkill(_characterRepository.Select(_characters.GetPlayerId()).GetSkills());
            var messageCode = _skillViewInfoFactory.Create(skill).Description;
            return _messageOutputDataFactory.Create(messageCode);
        }
    }
}