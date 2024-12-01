using System;
using System.Linq;
using System.Threading.Tasks;
using BattleScene.DataAccess;
using BattleScene.DataAccess.Dto;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.InterfaceAdapter.Presenter;
using BattleScene.InterfaceAdapter.State.Turn;
using static BattleScene.InterfaceAdapter.Presenter.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.InterfaceAdapter.Facade
{
    public class SkillOutputFacade
    {
        private readonly IResource<SkillViewDto, SkillCode> _skillViewResource;
        private readonly ICollection<BattleLogEntity, BattleLogId> _battleLogCollection;
        private readonly ICollection<CharacterEntity, CharacterId> _characterCollection;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;

        public SkillOutputFacade(
            IResource<SkillViewDto, SkillCode> skillViewResource,
            ICollection<BattleLogEntity, BattleLogId> battleLogCollection,
            ICollection<CharacterEntity, CharacterId> characterCollection,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView)
        {
            _skillViewResource = skillViewResource;
            _battleLogCollection = battleLogCollection;
            _characterCollection = characterCollection;
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public async Task Output(Context context)
        {
            if (context.ActorId == null) throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);
            if (context.Skill == null) throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);

            var messageCode = context.Skill.Common.AttackMessageCode;
            _messageView.StartAnimation(messageCode, context);

            var isActorPlayer = _characterCollection.Get(context.ActorId).IsPlayer;
            var playerSkillCode = isActorPlayer
                ? context.Skill.Common.SkillCode
                : _battleLogCollection.Get()
                    // 敵に先手を撃たれ混乱した状態で敵の攻撃を受ける場合、
                    // プレイヤーはまだスキルを一度も発動していないので、
                    // LastOrDefaultメソッドで戦闘履歴を取得する必要がある
                    .LastOrDefault(x => x.ActorId != null
                               && _characterCollection.Get(x.ActorId).IsPlayer
                               && !SkillCodeList.AilmentSkillCodeList.Contains(x.SkillCode))
                    ?.SkillCode ?? SkillCode.Attack;
            var playerImageCode = _skillViewResource.Get(playerSkillCode).PlayerImageCode;
            _playerImageView.StartAnimation(playerImageCode, Slide);
        }
    }
}