using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.FrameView.OutputBoundary;
using BattleScene.UseCases.View.FrameView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using BattleScene.UseCases.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCases.View.PlayerImageView.OutputDataFactory;
using BattleScene.UseCases.View.TechnicalPointBarView.OutputBoundary;
using BattleScene.UseCases.View.TechnicalPointBarView.OutputDaraFactory;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCases.UseCase
{
    internal class Attack
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IFrameViewPresenter _frameView;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly PlayerAttackPlayerImageOutputDataFactory _playerAttackPlayerImageOutputDataFactory;
        private readonly IPlayerImageViewPresenter _playerImageView;
        private readonly ISkillRepository _skillRepository;
        private readonly TargetFrameOutputDataFactory _targetFrameOutputDataFactory;
        private readonly TechnicalPointBarOutputDataFactory _technicalPointBarOutputDataFactory;
        private readonly ITechnicalPointBarViewPresenter _technicalPointBarView;
        private readonly IFactory<SkillValueObject, SkillCode> _skillFactory;

        public Attack(
            ICharacterRepository characterRepository,
            IFrameViewPresenter frameView,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView,
            OrderedItemsDomainService orderedItems,
            PlayerAttackPlayerImageOutputDataFactory playerAttackPlayerImageOutputDataFactory,
            IPlayerImageViewPresenter playerImageView,
            ISkillRepository skillRepository,
            TargetFrameOutputDataFactory targetFrameOutputDataFactory,
            TechnicalPointBarOutputDataFactory technicalPointBarOutputDataFactory,
            ITechnicalPointBarViewPresenter technicalPointBarView,
            IFactory<SkillValueObject, SkillCode> skillFactory)
        {
            _characterRepository = characterRepository;
            _frameView = frameView;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
            _orderedItems = orderedItems;
            _playerAttackPlayerImageOutputDataFactory = playerAttackPlayerImageOutputDataFactory;
            _playerImageView = playerImageView;
            _skillRepository = skillRepository;
            _targetFrameOutputDataFactory = targetFrameOutputDataFactory;
            _technicalPointBarOutputDataFactory = technicalPointBarOutputDataFactory;
            _technicalPointBarView = technicalPointBarView;
            _skillFactory = skillFactory;
        }

        public void Execute()
        {
            var characterId = _orderedItems.FirstCharacterId();
            var skill = _skillRepository.Select(characterId);

            if (_characterRepository.Select(characterId).IsPlayer())
            {
                // TODO: FatalitySkillかどうか判定し、リミットゲージの表示更新を可否を決定する

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
                    _skillFactory.Create(_skillRepository.Select(_orderedItems.FirstCharacterId()).SkillCode)
                        .SkillCommon.MessageCode;
                var messageOutputData = _messageOutputDataFactory.Create(messageCode);
                _messageView.Start(messageOutputData);
            }
        }

        public EventCode NextEvent()
        {
            return EventCode.SwitchSkillEvent;
        }
    }
}