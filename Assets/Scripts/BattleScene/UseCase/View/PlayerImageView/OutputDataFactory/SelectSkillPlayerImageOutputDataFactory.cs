using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.View.PlayerImageView.OutputData;

namespace BattleScene.UseCase.View.PlayerImageView.OutputDataFactory
{
    public class SelectSkillPlayerImageOutputDataFactory
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly CharactersDomainService _characters;
        private readonly ISkillSelectorRepository _skillSelectorRepository;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;

        public SelectSkillPlayerImageOutputDataFactory(
            ICharacterRepository characterRepository,
            CharactersDomainService characters,
            ISkillSelectorRepository skillSelectorRepository,
            ISkillViewInfoFactory skillViewInfoFactory)
        {
            _characterRepository = characterRepository;
            _characters = characters;
            _skillSelectorRepository = skillSelectorRepository;
            _skillViewInfoFactory = skillViewInfoFactory;
        }

        public PlayerImageOutputData Create(EventCode eventCode)
        {
            var skillSelector = _skillSelectorRepository.Select(new SkillSelectorId(eventCode));
            var skill = skillSelector.GetSkill(_characterRepository.Select(_characters.GetPlayerId()).GetSkills());
            var playerImageCode = _skillViewInfoFactory.Create(skill).PlayerImageCode;
            return new PlayerImageOutputData(playerImageCode);
        }
    }
}