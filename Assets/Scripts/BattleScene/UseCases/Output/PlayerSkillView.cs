using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.IPresenter;

namespace BattleScene.UseCases.Output
{
    public class PlayerSkillView
    {
        private readonly CharactersDomainService _characters;
        private readonly ISkillRepository _skillRepository;
        private readonly IPlayerSkillViewPresenter _playerSkillView;

        public PlayerSkillView(
            CharactersDomainService characters,
            ISkillRepository skillRepository,
            IPlayerSkillViewPresenter playerSkillView)
        {
            _characters = characters;
            _skillRepository = skillRepository;
            _playerSkillView = playerSkillView;
        }

        public void Out()
        {
            var playerId = _characters.GetPlayerId();
            var skill = _skillRepository.Select(playerId);
            _playerSkillView.Out(skill);
        }
    }
}