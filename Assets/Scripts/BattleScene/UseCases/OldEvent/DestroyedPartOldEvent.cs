using System;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View.DestroyedPartView.OutputBoundary;
using BattleScene.UseCases.View.DestroyedPartView.OutputDataFactory;
using BattleScene.UseCases.View.MessageView.OutputBoundary;
using BattleScene.UseCases.View.MessageView.OutputDataFactory;

namespace BattleScene.UseCases.OldEvent
{
    internal class DestroyedPartOldEvent : IOldEvent, IWait
    {
        private readonly IBodyPartRepository _bodyPartRepository;
        private readonly DestroyedPartGeneratorService _destroyedPartGenerator;
        private readonly DestroyedPartOutputDataFactory _destroyedPartOutputDataFactory;
        private readonly IDestroyedPartViewPresenter _destroyedPartView;
        private readonly MessageOutputDataFactory _messageOutputDataFactory;
        private readonly IMessageViewPresenter _messageView;
        private readonly ResultDomainService _result;
        private readonly IRepository<BattleLogEntity, BattleLogId> _battleLogRepository;
        

        public DestroyedPartOldEvent(
            IBodyPartRepository bodyPartRepository,
            DestroyedPartGeneratorService destroyedPartGenerator,
            DestroyedPartOutputDataFactory destroyedPartOutputDataFactory,
            IDestroyedPartViewPresenter destroyedPartView,
            MessageOutputDataFactory messageOutputDataFactory,
            IMessageViewPresenter messageView,
            ResultDomainService result,
            IRepository<BattleLogEntity, BattleLogId> battleLogRepository)
        {
            _bodyPartRepository = bodyPartRepository;
            _destroyedPartGenerator = destroyedPartGenerator;
            _destroyedPartOutputDataFactory = destroyedPartOutputDataFactory;
            _destroyedPartView = destroyedPartView;
            _messageOutputDataFactory = messageOutputDataFactory;
            _messageView = messageView;
            _result = result;
            _bodyPartRepository = bodyPartRepository;
        }

        public EventCode Run()
        {
            // if (_skillRepository.Select(_orderedItems.FirstCharacterId())
            //         .DequeSkillElement() is not IDestroyedPartSkill)
            //     throw new InvalidCastException();
            //
            // var destroyedPartSkillResult = _result.Last<DestroyedPartValueObject>();
            //
            //
            // // ダメージを与えるスキルで部位破壊に失敗したとき、失敗のメッセージを表示せず次のイベントに移る
            // // 失敗のメッセージを表示しているとゲームのテンポが悪くなるため
            // if (_result.TryGetLast<DamageValueObject>(out _)
            //     && !destroyedPartSkillResult.Success()) return GetIndex();
            //
            // if (!destroyedPartSkillResult.Success())
            // {
            //     // TODO: MessageOutputDataを取得する処理を記述する
            //     // TODO: メッセージを設定する
            //     _messageView.Start(new MessageOutputData("NoMessage"));
            //     return WaitEvent;
            // }
            //
            // var bodyPartList = _bodyPartRepository.Select(destroyedPartSkillResult.CharacterId);
            // var newBodyPart = _destroyedPartGenerator.Create(bodyPartList, destroyedPartSkillResult);
            // _bodyPartRepository.Update(newBodyPart);
            //
            // var destroyedPartOutputData = _destroyedPartOutputDataFactory.Create(destroyedPartSkillResult.CharacterId);
            // _destroyedPartView.Start(destroyedPartOutputData);
            //
            // // TODO: メッセージを設定する
            // var messageOutputData = _messageOutputDataFactory.Create(NoMessage);
            // _messageView.Start(messageOutputData);
            // return WaitEvent;
            throw new NotImplementedException();
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