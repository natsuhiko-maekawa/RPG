﻿using System;
using System.Linq;
using BattleScene.DataAccesses;
using BattleScene.DataAccesses.Dto;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Presenters.Presenters;
using BattleScene.Presenters.States.Turn;
using static BattleScene.Presenters.Presenters.PlayerImageViewPresenter.AnimationMode;

namespace BattleScene.Presenters.PresenterFacades
{
    public class SkillPresenterFacade
    {
        private readonly IResource<SkillViewDto, SkillCode> _skillViewResource;
        private readonly IRepository<BattleEventEntity, BattleEventId> _battleLogRepository;
        private readonly MessageViewPresenter _messageView;
        private readonly PlayerImageViewPresenter _playerImageView;

        public SkillPresenterFacade(
            IResource<SkillViewDto, SkillCode> skillViewResource,
            IRepository<BattleEventEntity, BattleEventId> battleLogRepository,
            MessageViewPresenter messageView,
            PlayerImageViewPresenter playerImageView)
        {
            _skillViewResource = skillViewResource;
            _battleLogRepository = battleLogRepository;
            _messageView = messageView;
            _playerImageView = playerImageView;
        }

        public void Output(Context context)
        {
            var actor = context.Actor ?? throw new InvalidOperationException(ExceptionMessage.ContextActorIdIsNull);
            var skill = context.Skill ?? throw new InvalidOperationException(ExceptionMessage.ContextSkillIsNull);

            var messageCode = skill.Common.AttackMessageCode;
            _messageView.StartAnimation(messageCode);

            var playerSkillCode = actor.IsPlayer
                ? skill.Common.SkillCode
                : _battleLogRepository.Get()
                    // 敵に先手を撃たれ混乱した状態で敵の攻撃を受ける場合、
                    // プレイヤーはまだスキルを一度も発動していないので、
                    // LastOrDefaultメソッドで戦闘履歴を取得する必要がある
                    .LastOrDefault(static battleEvent => battleEvent.Actor is { IsPlayer: true }
                                                         && !SkillCodeList.AilmentSkillCodeList
                                                             .Contains(battleEvent.SkillCode))
                    ?.SkillCode ?? SkillCode.Attack;
            var playerImageCode = _skillViewResource.Get(playerSkillCode).PlayerImageCode;
            _playerImageView.StartAnimation(playerImageCode, Slide);
        }
    }
}