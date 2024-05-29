using System;
using System.Collections.Generic;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.View.FrameView.OutputBoundary;
using BattleScene.UseCase.View.FrameView.OutputDataFactory;
using BattleScene.UseCase.View.InfoView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using UnityEngine;
using static BattleScene.UseCase.EventRunner.EventCode;
using static BattleScene.Domain.Code.Range;
using static BattleScene.UseCase.Constant;
using static BattleScene.Domain.Code.MessageCode;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.UseCase.Event
{
    internal class SelectTargetEvent : IEvent, IWait, ISelectable, ICancelable
    {
        private readonly CharactersDomainService _characters;
        private readonly TargetFrameOutputDataFactory _targetFrameOutputDataFactory;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly ISelectorRepository _selectorRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ITargetRepository _targetRepository;
        private readonly IFrameViewPresenter _frameView;
        private readonly IInfoViewPresenter _infoView;
        private readonly IMessageViewPresenter _messageView;

        public EventCode Run()
        {
            var selectorId = new SelectorId(EventCode.SelectTargetEvent);
            var enemyNumber = _characters.GetEnemiesId().Count;
            var selector = new SelectorAggregate(selectorId, enemyNumber, enemyNumber);
            _selectorRepository.Update(selector);

            var playerId = _characters.GetPlayerId();
            var skill = _skillRepository.Select(playerId);
            var targetEntity = _targetRepository.Select(playerId);
            switch (skill.AbstractSkill.GetRange())
            {
                case Solo:
                    targetEntity.Set(new List<CharacterId> { _characters.GetEnemiesId()[selector.GetSelection()] });
                    break;
                case Line:
                case Range.Random:
                    targetEntity.Set(_characters.GetEnemiesId());
                    break;
                case Oneself:
                    targetEntity.Set(new List<CharacterId> { _characters.GetPlayerId() });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _targetRepository.Update(targetEntity);

            StartSelectTargetView();
            _infoView.StartInfoView(SelectTargetInfo);

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            _frameView.Stop();
            _infoView.Stop();
            return EventCode.PlayerAttackEvent;
        }

        public void CancelAction()
        {
            _frameView.Stop();
        }

        public void SelectAction(Vector2 direction)
        {
            var playerId = _characters.GetPlayerId();
            var skill = _skillRepository.Select(playerId);
            if (skill.AbstractSkill.GetRange() != Solo) return;
            _frameView.Stop();
            var targetEntity = _targetRepository.Select(playerId);
            var selector = _selectorRepository.Select(new SelectorId(EventCode.SelectTargetEvent));
            switch (direction.x)
            {
                case > 0: // 右入力時
                    selector.Right();
                    targetEntity.Set(new List<CharacterId> { _characters.GetEnemiesId()[selector.GetSelection()] });
                    StartSelectTargetView();
                    break;
                case < 0: // 左入力時
                    selector.Left();
                    targetEntity.Set(new List<CharacterId> { _characters.GetEnemiesId()[selector.GetSelection()] });
                    StartSelectTargetView();
                    break;
            }

            _targetRepository.Update(targetEntity);
            _selectorRepository.Update(selector);
        }

        private void StartSelectTargetView()
        {
            var targetFrameOutputData = _targetFrameOutputDataFactory.Create();
            _frameView.Start(targetFrameOutputData);
            var messageOutputData = _messageOutputDataFactory.Create(SelectTargetMessage, noWait:true);
            _messageView.Start(messageOutputData);
        }
    }
}