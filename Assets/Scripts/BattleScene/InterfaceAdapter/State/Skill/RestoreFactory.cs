using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class RestoreFactory
    {
        public RestoreState Create(RestoreParameterValueObject restoreParameter) => new RestoreState();
    }
}