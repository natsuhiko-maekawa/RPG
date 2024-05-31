using System;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.View.PlayerImageView.OutputData;

namespace BattleScene.UseCase.OutputDataFactory
{
    [Obsolete]
    public class PlayerAttackEventOutputDataFactory
    {
        private readonly IAilmentViewInfoFactory _ailmentViewInfoFactory;
        private readonly CharactersDomainService _characters;
        private readonly ISkillRepository _skillRepository;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;

        public PlayerImageOutputData CreatePlayerImageOutputData()
        {
            var playerId = _characters.GetPlayerId();
            var skillCode = _skillRepository.Select(playerId).SkillCode;
            var playerImageCode = _skillViewInfoFactory.Create(skillCode).PlayerImageCode;
            return new PlayerImageOutputData(playerImageCode);
        }
    }
}