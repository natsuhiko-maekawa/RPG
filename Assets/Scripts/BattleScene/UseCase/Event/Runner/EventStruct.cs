using System.Collections.Generic;
using System.Collections.Immutable;

namespace BattleScene.UseCase.Event.Runner
{
    public struct EventStruct
    {
        private ImmutableList<EventCode> EventCodeList { get; }

        // TODO: Controller, Presenterも追加する
        
        public EventStruct(IList<EventCode> eventCodeList)
        {
            EventCodeList = eventCodeList.ToImmutableList();
        }
    }
}