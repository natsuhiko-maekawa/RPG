using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.View.PlayerImageView.OutputData;

namespace BattleScene.UseCase.View.PlayerImageView.OutputDataFactory
{
    public class PlayerAttackPlayerImageOutputDataFactory
    {
        private readonly IAilmentViewInfoFactory _ailmentViewInfoFactory;
        private readonly ISkillRepository _skillRepository;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;
        private readonly CharactersDomainService _characters;
        
        public PlayerImageOutputData Create()
        {
            var playerId = _characters.GetPlayerId();
            var skillCode = _skillRepository.Select(playerId).SkillCode;
            var playerImageCode = _skillViewInfoFactory.Create(skillCode).PlayerImageCode;
            return new PlayerImageOutputData(playerImageCode);
        }
    }
}