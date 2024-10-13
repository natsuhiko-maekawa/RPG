using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class ResetSkillResultValueObject
    {
        public ResetSkillResultValueObject(
            CharacterId actorId,
            SkillCode skillCode,
            IReadOnlyList<AilmentCode> ailmentCodeList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            AilmentCodeList = ailmentCodeList;
            TargetIdList = targetIdList;
        }

        public IReadOnlyList<AilmentCode> AilmentCodeList { get; }
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }

        public bool Success()
        {
            throw new NotImplementedException();
        }
    }
}