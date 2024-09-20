using BattleScene.UseCases.OldEvent.Interface;
using BattleScene.UseCases.OldEvent.Runner;

namespace BattleScene.UseCases.OldEvent
{
    public class AilmentSkillFailureViewOldEvent : IOldEvent, IWait
    {
        public EventCode Run()
        {
            // メッセージを表示
            // var failAilmentsMessageOutputData = _messageOutputDataFactory.Create(MessageCode.FailAilmentMessage);
            // _messageView.Start(failAilmentsMessageOutputData);
            return EventCode.WaitEvent;
        }

        public EventCode NextEvent()
        {
            return EventCode.SwitchSkillEvent;
        }
    }
}