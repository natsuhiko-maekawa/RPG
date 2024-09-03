using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View.InfoView.OutputBoundary;
using BattleScene.UseCases.View.SelectActionView.OutputBoundary;
using UnityEngine;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;
using static BattleScene.UseCases.Constant;
using static BattleScene.Domain.Code.SkillCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class SelectActionOldEvent : IOldEvent, IWait, ISelectable
    {
        private readonly AttackCounterService _attackCounter;
        private readonly CharactersDomainService _characters;
        private readonly IInfoViewPresenter _infoView;
        private readonly ISelectActionViewPresenter _selectActionView;
        private readonly ISelectorRepository _selectorRepository;
        private readonly SkillCreatorService _skillCreator;
        private readonly ISkillRepository _skillRepository;

        public SelectActionOldEvent(
            AttackCounterService attackCounter,
            CharactersDomainService characters,
            IInfoViewPresenter infoView,
            ISelectActionViewPresenter selectActionView,
            ISelectorRepository selectorRepository,
            SkillCreatorService skillCreator,
            ISkillRepository skillRepository)
        {
            _attackCounter = attackCounter;
            _characters = characters;
            _infoView = infoView;
            _selectActionView = selectActionView;
            _selectorRepository = selectorRepository;
            _skillCreator = skillCreator;
            _skillRepository = skillRepository;
        }

        public EventCode Run()
        {
            _infoView.StartInfoView(SelectActionInfo);
            StartView();

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
                    skill = _skillCreator.Create(playerId, SkillCode.Attack);
                    _skillRepository.Update(skill);
                    return EventCode.SelectTargetEvent;
                case 1:
                    return EventCode.SelectSkillEvent;
                case 2:
                    skill = _skillCreator.Create(playerId, Defence);
                    _skillRepository.Update(skill);
                    return PlayerDefenceEvent;
                case 3 when _attackCounter.IsOverflow():
                    return EventCode.SelectFatalitySkillEvent;
            }

            return WaitEvent;
        }

        private void StartView()
        {
            // _selectActionView.Start(_outputDataFactory.CreateSelectActionOutputData());
            // _messageView.Start(_outputDataFactory.CreateMessageOutputDataFactory());
            // _playerImageView.Start(_outputDataFactory.CreatePlayerImageOutputData());
        }
    }
}