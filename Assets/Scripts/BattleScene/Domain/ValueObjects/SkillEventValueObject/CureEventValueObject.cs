using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObjects.SkillEventValueObject
{
    public class CureEventValueObject : ISkillEventValueObject // 24byte
    {
        public SkillEventCode SkillEventCode { get; }
        public IReadOnlyList<CharacterEntity> TargetList { get; }
        public IReadOnlyList<CuringValueObject> CuringList { get; }

        public CureEventValueObject(
            IReadOnlyList<CharacterEntity> targetList,
            IReadOnlyList<CuringValueObject> curingList)
        {
            SkillEventCode = SkillEventCode.Cure;
            TargetList = targetList;
            CuringList = curingList;
        }
    }
}