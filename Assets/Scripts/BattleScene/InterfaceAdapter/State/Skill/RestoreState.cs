using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class RestoreState : AbstractSkillState
    {
        private SkillCommonValueObject _skillCommon;
        private RestoreParameterValueObject _restoreParameter;

        public RestoreState(
            SkillCommonValueObject skillCommon,
            RestoreParameterValueObject restoreParameter)
        {
            _skillCommon = skillCommon;
            _restoreParameter = restoreParameter;
        }
    }
}