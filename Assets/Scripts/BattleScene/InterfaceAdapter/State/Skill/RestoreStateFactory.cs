using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class RestoreStateFactory
    {
        public RestoreState Create(
            SkillCommonValueObject skillCommon,
            RestoreParameterValueObject restoreParameter) => new RestoreState(
            skillCommon: skillCommon,
            restoreParameter: restoreParameter);
    }
}