using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.State.Skill
{
    public class BuffStateFactory
    {
        public BuffState Create(
            SkillCommonValueObject skillCommon,
            BuffValueObject buff) => new BuffState(
            skillCommon: skillCommon,
            buff: buff);
    }
}