using BattleScene.UseCases.OldEvent.Runner;
using static BattleScene.UseCases.OldEvent.Runner.EventCode;

namespace BattleScene.UseCases.OldEvent
{
    internal class BuffOldEvent
    {
        public EventCode Run()
        {
            // if (_skillRepository.Select(_orderedItems.FirstCharacterId()).Skill is not IBuffSkill)
            //     throw new InvalidCastException();
            //
            // var buffSkillResult = _result.Last<BuffValueObject>();
            //
            // var buffOutputData = _buffOutputDataFactory.Create(buffSkillResult.TargetIdList);
            // _buffView.Start(buffOutputData);
            //
            // var buffViewInfo = _buffViewInfoFactory.Create(buffSkillResult.BuffCode);
            // var messageOutputData = _messageOutputDataFactory.Create(buffViewInfo.MessageCode);
            // _messageView.Start(messageOutputData);

            return WaitEvent;
        }

        public EventCode NextEvent()
        {
            return LoopEndEvent;
        }
    }
}