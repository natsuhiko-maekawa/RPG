using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class SkillValueObject
    {
        public SkillCode SkillCode { get; }
        public Range Range { get; }
        public ImmutableList<AilmentValueObject> AilmentList { get; }
        public ImmutableList<BuffValueObject> BuffList { get; }

        public SkillValueObject(
            SkillCode skillCode,
            Range range,
            ImmutableList<AilmentValueObject> ailmentList,
            ImmutableList<BuffValueObject> buffList)
        {
            SkillCode = skillCode;
            Range = range;
            AilmentList = ailmentList;
            BuffList = buffList;
        }
    }
}