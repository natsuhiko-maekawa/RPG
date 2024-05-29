using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.OutputDataFactory;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.InfoView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCase.View.SelectActionView.OutputBoundary;
using UnityEngine;
using static BattleScene.UseCase.EventRunner.EventCode;
using static BattleScene.UseCase.Constant;
using static BattleScene.Domain.Code.SkillCode;

namespace BattleScene.UseCase.Event
{
    internal class SelectActionEvent : IEvent, IWait, ISelectable
    {
        private readonly AttackCounterService _attackCounter;
        private readonly CharactersDomainService _characters;
        private readonly SkillCreatorService _skillCreator;
        private readonly SelectActionEventOutputDataFactory _outputDataFactory;
        private readonly ISelectorRepository _selectorRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IInfoViewPresenter _infoView;
        private readonly IMessageViewPresenter _messageView;
        private readonly IPlayerImageViewPresenter _playerImageView;
        private readonly ISelectActionViewPresenter _selectActionView;
        
        public EventCode Run()
        {
            _infoView.StartInfoView(SelectActionInfo);
            StartView();
            
            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            _selectActionView.Stop();
            _infoView.Stop();
            var playerId = _characters.GetPlayerId();
            SkillEntity skill;
            var selector = _selectorRepository.Select(new SelectorId(EventCode.SelectActionEvent));
            switch (selector.GetSelection())
            {
                case 0:
                    skill = _skillCreator.Create(playerId, Attack);
                    _skillRepository.Update(skill);
                    return EventCode.SelectTargetEvent;
                case 1:
                    return EventCode.SelectSkillEvent;
                case 2:
                    skill = _skillCreator.Create(playerId, Defence);
                    _skillRepository.Update(skill);
                    return EventCode.PlayerDefenceEvent;
                case 3 when _attackCounter.IsOverflow():
                    return EventCode.SelectFatalitySkillEvent;
            }

            return WaitEvent;
        }

        public void SelectAction(Vector2 direction)
        {
            var selector = _selectorRepository.Select(new SelectorId(EventCode.SelectActionEvent));
            switch (direction.y)
            {
                case > 0: // 上入力時
                    selector.Up();
                    StartView();
                    break;
                case < 0: // 下入力時
                    selector.Down();
                    StartView();
                    break;
            }
            
            _selectorRepository.Update(selector);
        }

        private void StartView()
        {
            _selectActionView.Start(_outputDataFactory.CreateSelectActionOutputData());
            _messageView.Start(_outputDataFactory.CreateMessageOutputDataFactory());
            _playerImageView.Start(_outputDataFactory.CreatePlayerImageOutputData());
        }
    }
}