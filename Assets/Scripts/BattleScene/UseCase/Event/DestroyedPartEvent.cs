using System;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.Event.Interface;
using BattleScene.UseCase.EventRunner;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.Skill.Interface;
using BattleScene.UseCase.View.DestroyedPartView.OutputBoundary;
using BattleScene.UseCase.View.DestroyedPartView.OutputDataFactory;
using BattleScene.UseCase.View.MessageView.OutputBoundary;
using BattleScene.UseCase.View.MessageView.OutputData;
using BattleScene.UseCase.View.MessageView.OutputDataFactory;
using static BattleScene.UseCase.EventRunner.EventCode;
using static BattleScene.Domain.Code.MessageCode;

namespace BattleScene.UseCase.Event
{
    internal class DestroyedPartEvent : IEvent, IWait
    {
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ResultDomainService _result;
        private readonly DestroyedPartCreatorService _destroyedPartCreator;
        private readonly DestroyedPartOutputDataFactory _destroyedPartOutputDataFactory;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IBodyPartRepository _bodyPartRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IDestroyedPartViewPresenter _destroyedPartView;
        private readonly IMessageViewPresenter _messageView;

        public EventCode Run()
        {
            if (_skillRepository.Select(_orderedItems.FirstCharacterId()).DequeSkillElement() is not IDestroyedPartSkill)
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