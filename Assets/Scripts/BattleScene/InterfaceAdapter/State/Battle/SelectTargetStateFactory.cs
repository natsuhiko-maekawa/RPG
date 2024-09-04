using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.State.Battle
{
    public class SelectTargetStateFactory
    {
        public SelectTargetState Create(ActionCode actionCode)
        {
            return new SelectTargetState();
        }
    }
}