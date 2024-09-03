using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess.ObsoleteIFactory;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View.MessageView.OutputData;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using BattleScene.UseCases.View.PlayerImageView.OutputData;
using BattleScene.UseCases.View.SelectActionView.OutputData;

namespace BattleScene.UseCases.OutputDataFactory
{
    [Obsolete]
    public class SelectActionEventOutputDataFactory
    {
        private readonly AttackCounterService _attackCounter;
        private readonly CharactersDomainService _characters;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly ISelectorRepository _selectorRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;

        public SelectActionOutputData CreateSelectActionOutputData()
        {
            throw new NotImplementedException();
        }

        public MessageOutputData CreateMessageOutputDataFactory()
        {
            var selector = _selectorRepository.Select(new SelectorId(EventCode.SelectActionEvent));
            var messageEnum = selector.GetSelector().Selection switch
            {
                0 => MessageCode.AttackDescription,
                1 => MessageCode.SkillDescription,
                2 => MessageCode.DefenceDescription,
                3 => MessageCode.FatalitySkillDescription,
                _ => MessageCode.NoMessage
            };

            return _messageOutputDataFactory.Create(messageEnum);
        }

        public PlayerImageOutputData CreatePlayerImageOutputData()
        {
            var skillCode = _skillRepository.Select(_characters.GetPlayerId()).SkillCode;
            var playerImageCode = _skillViewInfoFactory.Create(skillCode).PlayerImageCode;
            return new PlayerImageOutputData(playerImageCode);
        }
    }
}