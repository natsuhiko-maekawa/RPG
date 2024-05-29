using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.View.AttackCountView.OutputBoundary;
using BattleScene.UseCase.View.AttackCountView.OutputDataFactory;
using BattleScene.UseCase.View.FrameView.OutputBoundary;
using BattleScene.UseCase.View.FrameView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using BattleScene.UseCase.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCase.View.PlayerImageView.OutputDataFactory;
using BattleScene.UseCase.View.TechnicalPointBarView.OutputBoundary;
using BattleScene.UseCase.View.TechnicalPointBarView.OutputDaraFactory;
using static BattleScene.UseCase.EventRunner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCase.Event
{
    internal class PlayerAttackEvent : IEvent, IWait
    {
        private readonly AttackCountOutputDataFactory _attackCountOutputDataFactory;
        private readonly IAttackCountViewPresenter _attackCountView;
        private readonly ICharacterRepository _characterRepository;
        private readonly IFrameViewPresenter _frameView;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly PlayerAttackPlayerImageOutputDataFactory _playerAttackPlayerImageOutputDataFactory;
        private readonly IPlayerImageViewPresenter _playerImageView;
        private readonly ResultDomainService _result;
        private readonly ISkillRepository _skillRepository;
        private readonly ISkillViewInfoFactory _skillViewInfoFactory;
        private readonly TargetFrameOutputDataFactory _targetFrameOutputDataFactory;
        private readonly TechnicalPointBarOutputDataFactory _technicalPointBarOutputDataFactory;
        private readonly ITechnicalPointBarViewPresenter _technicalPointBarView;
        private readonly ITechnicalPointRepository _technicalPointRepository;


        public EventCode Run()
        {
            var characterId = _orderedItems.FirstCharacterId();
            var skill = _skillRepository.Select(characterId);

            if (_characterRepository.Select(characterId).IsPlayer())
            {
                var technicalPoint = _technicalPointRepository.Select();
                technicalPoint.Reduce(skill.AbstractSkill.GetTechnicalPoint());
                _technicalPointRepository.Update(technicalPoint);
            }

            if (_characterRepository.Select(characterId).IsPlayer())
            {
                // TODO: FatalitySkillかどうか判定する
                if (false) _attackCountView.Start(_attackCountOutputDataFactory.Create());

                var targetFrameOutputData = _targetFrameOutputDataFactory.Create();
                _frameView.Start(targetFrameOutputData);

                var playerImageOutputData = _playerAttackPlayerImageOutputDataFactory.Create();
                _playerImageView.Start(playerImageOutputData);

                var technicalPointBarOutputData = _technicalPointBarOutputDataFactory.Create();
                _technicalPointBarView.Start(technicalPointBarOutputData);

                var messageCode = skill.GetType().ToString().Split(".").Last() == "AttackSkill"
                    ? AttackMessage
                    : SkillMessage;
                var messageOutputData = _messageOutputDataFactory.Create(messageCode);
                _messageView.Start(messageOutputData);
            }
            else
            {
                var messageCode =
                    _skillViewInfoFactory.Create(_skillRepository.Select(_orderedItems.FirstCharacterId()).SkillCode)
                        .MessageCode;
                var messageOutputData = _messageOutputDataFactory.Create(messageCode);
                _messageView.Start(messageOutputData);
            }

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.SwitchSkillEvent;
        }
    }
}