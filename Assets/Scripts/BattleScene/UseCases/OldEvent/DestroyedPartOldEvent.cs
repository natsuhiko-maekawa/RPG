using System;
using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;

namespace BattleScene.UseCases.OldEvent
{
    internal class DestroyedPartOldEvent : IOldEvent, IWait
    {
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