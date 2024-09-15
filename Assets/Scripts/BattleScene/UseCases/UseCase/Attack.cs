using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.View.FrameView.OutputBoundary;
using BattleScene.UseCases.View.FrameView.OutputDataFactory;
using BattleScene.UseCases.View.TechnicalPointBarView.OutputBoundary;
using BattleScene.UseCases.View.TechnicalPointBarView.OutputDaraFactory;

namespace BattleScene.UseCases.UseCase
{
    internal class Attack
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IFrameViewPresenter _frameView;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly TargetFrameOutputDataFactory _targetFrameOutputDataFactory;
        private readonly TechnicalPointBarOutputDataFactory _technicalPointBarOutputDataFactory;
        private readonly ITechnicalPointBarViewPresenter _technicalPointBarView;

        public Attack(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IFrameViewPresenter frameView,
            OrderedItemsDomainService orderedItems,
            TargetFrameOutputDataFactory targetFrameOutputDataFactory,
            TechnicalPointBarOutputDataFactory technicalPointBarOutputDataFactory,
            ITechnicalPointBarViewPresenter technicalPointBarView)
        {
            _characterRepository = characterRepository;
            _frameView = frameView;
            _orderedItems = orderedItems;
            _targetFrameOutputDataFactory = targetFrameOutputDataFactory;
            _technicalPointBarOutputDataFactory = technicalPointBarOutputDataFactory;
            _technicalPointBarView = technicalPointBarView;
        }

        public void Execute()
        {
            var characterId = _orderedItems.FirstCharacterId();
            // var skill = _skillRepository.Select(characterId);

            if (_characterRepository.Select(characterId).IsPlayer)
            {
                // TODO: FatalitySkillかどうか判定し、リミットゲージの表示更新を可否を決定する

                var targetFrameOutputData = _targetFrameOutputDataFactory.Create();
                _frameView.Start(targetFrameOutputData);

                // プレイヤーの立ち絵を表示
                // var playerImageOutputData = _playerAttackPlayerImageOutputDataFactory.Create();
                // _playerImageView.Start(playerImageOutputData);

                var technicalPointBarOutputData = _technicalPointBarOutputDataFactory.Create();
                _technicalPointBarView.Start(technicalPointBarOutputData);

                // 通常攻撃の場合、通常攻撃のメッセージを表示
                // スキルの場合、スキルのメッセージを表示
                // var messageCode = skill.GetType().ToString().Split(".").Last() == "AttackSkill"
                //     ? AttackMessage
                //     : SkillMessage;
                // var messageOutputData = _messageOutputDataFactory.Create(messageCode);
                // _messageView.Start(messageOutputData);
            }
            else
            {
                // スキルメッセージを表示
                // var messageCode =
                //     _skillFactory.Create(_skillRepository.Select(_orderedItems.FirstCharacterId()).SkillCode)
                //         .SkillCommon.MessageCode;
                // var messageOutputData = _messageOutputDataFactory.Create(messageCode);
                // _messageView.Start(messageOutputData);
            }
        }

        public EventCode NextEvent()
        {
            return EventCode.SwitchSkillEvent;
        }
    }
}