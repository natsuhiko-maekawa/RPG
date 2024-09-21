using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.OldEvent.Runner;

namespace BattleScene.UseCases.UseCase
{
    internal class Attack
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;

        public Attack(
            IRepository<CharacterEntity, CharacterId> characterRepository,
            OrderedItemsDomainService orderedItems)
        {
            _characterRepository = characterRepository;
            _orderedItems = orderedItems;
        }

        public void Execute()
        {
            _orderedItems.First().TryGetCharacterId(out var characterId);
            // var skill = _skillRepository.Select(characterId);

            if (_characterRepository.Select(characterId).IsPlayer)
            {
                // TODO: FatalitySkillかどうか判定し、リミットゲージの表示更新を可否を決定する

                // 攻撃対象のフレームを表示していたが、不要
                // var targetFrameOutputData = _targetFrameOutputDataFactory.Create();
                // _frameView.Start(targetFrameOutputData);

                // プレイヤーの立ち絵を表示
                // var playerImageOutputData = _playerAttackPlayerImageOutputDataFactory.Create();
                // _playerImageView.Start(playerImageOutputData);

                // TPバーの表示を更新
                // var technicalPointBarOutputData = _technicalPointBarOutputDataFactory.Create();
                // _technicalPointBarView.Start(technicalPointBarOutputData);

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