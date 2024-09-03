using BattleScene.Domain.DataAccess.ObsoleteIFactory;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.PlayerImageView.OutputData;

namespace BattleScene.UseCases.View.PlayerImageView.OutputDataFactory
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