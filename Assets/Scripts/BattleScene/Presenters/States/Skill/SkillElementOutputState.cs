using System.Diagnostics.CodeAnalysis;
using BattleScene.Domain.Entities;

namespace BattleScene.Presenters.States.Skill
{
    public abstract class SkillElementOutputState<TSkillElement> : BaseState<TSkillElement>
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