using System;
using BattleScene.UseCases.OldEvent.Interface;
using VContainer;

namespace BattleScene.UseCases.OldEvent.Runner
{
    internal class EventFactory : IEventFactory
    {
        private readonly IObjectResolver _container;

        public EventFactory(IObjectResolver container)
        {
            _container = container;
        }

        public IOldEvent Create(EventCode eventCode)
        {
            return eventCode switch
            {
                EventCode.AilmentEvent => _container.Resolve<AilmentSkillOldEvent>(),
                EventCode.AilmentResetEvent => _container.Resolve<AilmentsResetOldEvent>(),
                EventCode.AilmentsSlipDamageEvent => _container.Resolve<SlipDamageOldEvent>(),
                EventCode.AilmentsSlipMessageEvent => _container.Resolve<SlipDamageMessageOldEvent>(),
                EventCode.BuffEvent => _container.Resolve<BuffOldEvent>(),
                EventCode.CantActionEvent => _container.Resolve<CantActionOldEvent>(),
                EventCode.CureEvent => _container.Resolve<CureOldEvent>(),
                EventCode.DestroyedPartEvent => _container.Resolve<DestroyedPartOldEvent>(),
                EventCode.EnemySuicideEvent => _container.Resolve<EnemySuicideOldEvent>(),
                EventCode.IsContinueEvent => _container.Resolve<IsContinueOldEvent>(),
                EventCode.LoopEndEvent => _container.Resolve<LoopEndOldEvent>(),
                EventCode.PlayerBeatEnemyEvent => _container.Resolve<PlayerBeatEnemyOldEvent>(),
                EventCode.PlayerDeadEvent => _container.Resolve<PlayerDeadOldEvent>(),
                EventCode.PlayerDamageEvent => _container.Resolve<PlayerDamageOldEvent>(),
                EventCode.PlayerWinEvent => _container.Resolve<PlayerWinOldEvent>(),
                EventCode.ResetEvent => _container.Resolve<ResetSkillOldEvent>(),
                EventCode.SelectActionEvent => _container.Resolve<SelectActionOldEvent>(),
                EventCode.SelectFatalitySkillEvent => _container.Resolve<SelectFatalitySkillOldEvent>(),
                EventCode.SelectSkillEvent => _container.Resolve<SelectSkillOldEvent>(),
                EventCode.SelectTargetEvent => _container.Resolve<SelectTargetOldEvent>(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}