using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObject.SkillEventValueObject
{
    public class CureEventValueObject : ISkillEventValueObject // 24byte
    {
        public SkillEventCode SkillEventCode { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }
        public IReadOnlyList<CharacterId> ActualTargetIdList => TargetIdList;
        public IReadOnlyList<CuringValueObject> CuringList { get; }

        public CureEventValueObject(
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CuringValueObject> curingList)
        {
            SkillEventCode = SkillEventCode.Cure;
            TargetIdList = targetIdList;
            CuringList = curingList;
        }
    }
}