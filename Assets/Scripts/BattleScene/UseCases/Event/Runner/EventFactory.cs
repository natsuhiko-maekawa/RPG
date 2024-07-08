using System;
using BattleScene.UseCases.Event.Interface;
using VContainer;

namespace BattleScene.UseCases.Event.Runner
{
    internal class EventFactory : IEventFactory
    {
        private readonly IObjectResolver _container;

        public EventFactory(IObjectResolver container)
        {
            _container = container;
        }

        public IEvent Create(EventCode eventCode)
        {
            return eventCode switch
            {
                EventCode.AilmentEvent => _container.Resolve<AilmentSkillEvent>(),
                EventCode.AilmentResetEvent => _container.Resolve<AilmentsResetEvent>(),
                EventCode.AilmentsSlipDamageEvent => _container.Resolve<SlipDamageEvent>(),
                EventCode.AilmentsSlipMessageEvent => _container.Resolve<SlipDamageMessageEvent>(),
                EventCode.BuffEvent => _container.Resolve<BuffEvent>(),
                EventCode.CantActionEvent => _container.Resolve<CantActionEvent>(),
                EventCode.CureEvent => _container.Resolve<CureEvent>(),
                EventCode.DestroyedPartEvent => _container.Resolve<DestroyedPartEvent>(),
                EventCode.EnemySuicideEvent => _container.Resolve<EnemySuicideEvent>(),
                EventCode.IsContinueEvent => _container.Resolve<IsContinueEvent>(),
                EventCode.LoopEndEvent => _container.Resolve<LoopEndEvent>(),
                EventCode.PlayerBeatEnemyEvent => _container.Resolve<PlayerBeatEnemyEvent>(),
                EventCode.PlayerDeadEvent => _container.Resolve<PlayerDeadEvent>(),
                EventCode.PlayerDamageEvent => _container.Resolve<PlayerDamageEvent>(),
                EventCode.PlayerWinEvent => _container.Resolve<PlayerWinEvent>(),
                EventCode.ResetEvent => _container.Resolve<ResetSkillEvent>(),
                EventCode.SelectActionEvent => _container.Resolve<SelectActionEvent>(),
                EventCode.SelectFatalitySkillEvent => _container.Resolve<SelectFatalitySkillEvent>(),
                EventCode.SelectSkillEvent => _container.Resolve<SelectSkillEvent>(),
                EventCode.SelectTargetEvent => _container.Resolve<SelectTargetEvent>(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}