using System;
using BattleScene.Domain.DataAccess.ObsoleteIFactory;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.View.PlayerImageView.OutputData;

namespace BattleScene.UseCases.OutputDataFactory
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