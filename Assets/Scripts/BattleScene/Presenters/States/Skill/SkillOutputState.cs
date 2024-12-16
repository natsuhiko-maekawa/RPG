using System.Diagnostics.CodeAnalysis;
using BattleScene.Domain.Entities;

namespace BattleScene.Presenters.States.Skill
{
    public abstract class SkillOutputState<TSkillComponent> : BaseState<TSkillComponent>
    {
        protected bool TryGetSuccessBattleEvent([NotNullWhen(true)] out BattleEventEntity? successBattleEvent)
        {
            successBattleEvent = null;
            while (Context.BattleEventQueue.TryDequeue(out var battleEvent))
            {
                if (battleEvent.IsFailure) continue;

                successBattleEvent = battleEvent;
                return true;
            }

            return false;
        }
    }
}