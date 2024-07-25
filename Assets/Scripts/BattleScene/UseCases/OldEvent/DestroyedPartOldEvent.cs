using System;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Interface;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View.DestroyedPartView.OutputBoundary;
using BattleScene.UseCases.View.DestroyedPartView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputData;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class DestroyedPartOldEvent : IOldEvent, IWait
    {
        private readonly IBodyPartRepository _bodyPartRepository;
        private readonly DestroyedPartCreatorService _destroyedPartCreator;
        private readonly DestroyedPartOutputDataFactory _destroyedPartOutputDataFactory;
        private readonly IDestroyedPartViewPresenter _destroyedPartView;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultDomainService _result;
        private readonly ISkillRepository _skillRepository;

        public DestroyedPartOldEvent(
            IBodyPartRepository bodyPartRepository,
            DestroyedPartCreatorService destroyedPartCreator,
            DestroyedPartOutputDataFactory destroyedPartOutputDataFactory,
            IDestroyedPartViewPresenter destroyedPartView,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView,
            OrderedItemsDomainService orderedItems,
            ResultDomainService result,
            ISkillRepository skillRepository)
        {
            _bodyPartRepository = bodyPartRepository;
            _destroyedPartCreator = destroyedPartCreator;
            _destroyedPartOutputDataFactory = destroyedPartOutputDataFactory;
            _destroyedPartView = destroyedPartView;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
            _orderedItems = orderedItems;
            _result = result;
            _skillRepository = skillRepository;
        }

        public EventCode Run()
        {
            // if (_skillRepository.Select(_orderedItems.FirstCharacterId())
            //         .DequeSkillElement() is not IDestroyedPartSkill)
                throw new InvalidCastException();

            var destroyedPartSkillResult = _result.Last<DestroyedPartSkillResultValueObject>();

            // ダメージを与えるスキルで部位破壊に失敗したとき、失敗のメッセージを表示せず次のイベントに移る
            // 失敗のメッセージを表示しているとゲームのテンポが悪くなるため
            if (_result.TryGetLast<DamageSkillResultValueObject>(out _)
                && !destroyedPartSkillResult.Success()) return GetIndex();

            if (!destroyedPartSkillResult.Success())
            {
                // TODO: MessageOutputDataを取得する処理を記述する
                // TODO: メッセージを設定する
                _messageView.Start(new MessageOutputData("NoMessage"));
                return WaitEvent;
            }

            var bodyPartList = _bodyPartRepository.Select(destroyedPartSkillResult.CharacterId);
            var newBodyPart = _destroyedPartCreator.Create(bodyPartList, destroyedPartSkillResult);
            _bodyPartRepository.Update(newBodyPart);

            var destroyedPartOutputData = _destroyedPartOutputDataFactory.Create(destroyedPartSkillResult.CharacterId);
            _destroyedPartView.Start(destroyedPartOutputData);

            // TODO: メッセージを設定する
            var messageOutputData = _messageOutputDataFactory.Create(NoMessage);
            _messageView.Start(messageOutputData);
            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return GetIndex();
        }

        private EventCode GetIndex()
        {
            return EventCode.SwitchSkillEvent;
        }
    }
}