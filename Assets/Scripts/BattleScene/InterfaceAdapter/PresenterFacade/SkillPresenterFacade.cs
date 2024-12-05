using System;
using System.Linq;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.State.Turn;
using static BattleScene.InterfaceAdapter.Presenter.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.PresenterFacade
{
    public class SkillPresenterFacade
    {
        private readonly IResource<SkillViewDto, SkillCode> _skillViewResource;
        private readonly IRepository<BattleEventEntity, BattleEventId> _battleLogRepository;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;

        public SkillPresenterFacade(
            IResource<SkillViewDto, SkillCode> skillViewResource,
            IRepository<BattleEventEntity, BattleEventId> battleLogRepository,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView)
        {
            _skillViewResource = skillViewResource;
            _battleLogRepository = battleLogRepository;
            _characterRepository = characterRepository;
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public void Output(Context context)
        {
            if (context.ActorId == null) throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);
            if (context.Skill == null) throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);

            var messageCode = context.Skill.Common.AttackMessageCode;
            _messageView.StartAnimation(messageCode, context);

            var isActorPlayer = _characterRepository.Get(context.ActorId).IsPlayer;
            var playerSkillCode = isActorPlayer
                ? context.Skill.Common.SkillCode
                : _battleLogRepository.Get()
                    // 敵に先手を撃たれ混乱した状態で敵の攻撃を受ける場合、
                    // プレイヤーはまだスキルを一度も発動していないので、
                    // LastOrDefaultメソッドで戦闘履歴を取得する必要がある
                    .LastOrDefault(x => x.ActorId != null
                               && _characterRepository.Get(x.ActorId).IsPlayer
                               && !SkillCodeList.AilmentSkillCodeList.Contains(x.SkillCode))
                    ?.SkillCode ?? SkillCode.Attack;
            var playerImageCode = _skillViewResource.Get(playerSkillCode).PlayerImageCode;
            _playerImageView.StartAnimation(playerImageCode, Slide);
        }
    }
}