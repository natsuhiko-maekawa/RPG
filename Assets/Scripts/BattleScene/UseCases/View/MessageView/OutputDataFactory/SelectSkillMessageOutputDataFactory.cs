using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Event.Runner;
using BattleScene.UseCases.View.MessageView.OutputData;

namespace BattleScene.UseCases.View.MessageView.OutputDataFactory
{
    public class SelectSkillMessageOutputDataFactory
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly CharactersDomainService _characters;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly ISkillSelectorRepository _skillSelectorRepository;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;

        public SelectSkillMessageOutputDataFactory(
            ICharacterRepository characterRepository,
            CharactersDomainService characters,
            MessageOutputDataFactory messageOutputDataFactory,
            ISkillSelectorRepository skillSelectorRepository,
            ISkillViewInfoFactory skillViewInfoFactory)
        {
            _characterRepository = characterRepository;
            _characters = characters;
            _messageOutputDataFactory = messageOutputDataFactory;
            _skillSelectorRepository = skillSelectorRepository;
            _skillViewInfoFactory = skillViewInfoFactory;
        }

        public MessageOutputData Create(EventCode eventCode)
        {
            var skillSelector = _skillSelectorRepository.Select(new SkillSelectorId(eventCode));
            var skill = skillSelector.GetSkill(_characterRepository.Select(_characters.GetPlayerId()).GetSkills());
            var messageCode = _skillViewInfoFactory.Create(skill).Description;
            return _messageOutputDataFactory.Create(messageCode);
        }
    }
}