using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.Event.Runner;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.InfoView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using BattleScene.UseCase.View.PlayerImageView.OutputBoundary;
using BattleScene.UseCase.View.PlayerImageView.OutputDataFactory;
using BattleScene.UseCase.View.SelectSkillView.OutputBoundary;
using BattleScene.UseCase.View.SelectSkillView.OutputDataFactory;
using UnityEngine;
using static BattleScene.UseCase.Event.Runner.EventCode;
using static BattleScene.UseCase.Constant;

namespace BattleScene.UseCase.Event
{
    internal class SelectSkillEvent : IEvent, IWait, ISelectable, ICancelable
    {
        private readonly CharactersDomainService _characters;
        private readonly IInfoViewPresenter _infoView;
        private readonly IMessageViewPresenter _messageView;
        private readonly IPlayerImageViewPresenter _playerImageView;
        private readonly SelectSkillService _selectSkill;
        private readonly SelectSkillMessageOutputDataFactory _selectSkillMessageOutputDataFactory;
        private readonly SelectSkillOutputDataFactory _selectSkillOutputDataFactory;
        private readonly SelectSkillPlayerImageOutputDataFactory _selectSkillPlayerImageOutputDataFactory;
        private readonly ISelectSkillViewPresenter _selectSkillView;
        private readonly SkillCreatorService _skillCreator;
        private readonly ISkillRepository _skillRepository;
        private readonly ISkillSelectorRepository _skillSelectorRepository;

        public SelectSkillEvent(
            CharactersDomainService characters,
            IInfoViewPresenter infoView,
            IMessageViewPresenter messageView,
            IPlayerImageViewPresenter playerImageView,
            SelectSkillService selectSkill,
            SelectSkillMessageOutputDataFactory selectSkillMessageOutputDataFactory,
            SelectSkillOutputDataFactory selectSkillOutputDataFactory,
            SelectSkillPlayerImageOutputDataFactory selectSkillPlayerImageOutputDataFactory,
            ISelectSkillViewPresenter selectSkillView,
            SkillCreatorService skillCreator,
            ISkillRepository skillRepository,
            ISkillSelectorRepository skillSelectorRepository)
        {
            _characters = characters;
            _infoView = infoView;
            _messageView = messageView;
            _playerImageView = playerImageView;
            _selectSkill = selectSkill;
            _selectSkillMessageOutputDataFactory = selectSkillMessageOutputDataFactory;
            _selectSkillOutputDataFactory = selectSkillOutputDataFactory;
            _selectSkillPlayerImageOutputDataFactory = selectSkillPlayerImageOutputDataFactory;
            _selectSkillView = selectSkillView;
            _skillCreator = skillCreator;
            _skillRepository = skillRepository;
            _skillSelectorRepository = skillSelectorRepository;
        }

        public void CancelAction()
        {
            _selectSkillView.Stop();
        }

        public EventCode Run()
        {
            _infoView.StartInfoView(SelectSkillInfo);
            StartView();

            return WaitEvent;
        }

        public void SelectAction(Vector2 direction)
        {
            var skillSelector = _skillSelectorRepository.Select(new SkillSelectorId(EventCode.SelectSkillEvent));
            switch (direction.y)
            {
                case > 0: // 上入力時
                    skillSelector.Up();
                    StartView();
                    break;
                case < 0: // 下入力時
                    skillSelector.Down();
                    StartView();
                    break;
            }

            _skillSelectorRepository.Update(skillSelector);
        }

        public EventCode NextEvent()
        {
            var canUpdate = _selectSkill.CanUpdate(EventCode.SelectSkillEvent);
            if (!canUpdate) return WaitEvent;

            var playerId = _characters.GetPlayerId();
            var skillCode = _selectSkill.GetSkillCode(EventCode.SelectSkillEvent);
            var skill = _skillCreator.Create(playerId, skillCode);
            _skillRepository.Update(skill);

            _infoView.Stop();
            _selectSkillView.Stop();

            return EventCode.SelectTargetEvent;
        }

        private void StartView()
        {
            _selectSkillView.Start(_selectSkillOutputDataFactory.Create(EventCode.SelectSkillEvent));
            _messageView.Start(_selectSkillMessageOutputDataFactory.Create(EventCode.SelectSkillEvent));
            _playerImageView.Start(_selectSkillPlayerImageOutputDataFactory.Create(EventCode.SelectSkillEvent));
        }
    }
}