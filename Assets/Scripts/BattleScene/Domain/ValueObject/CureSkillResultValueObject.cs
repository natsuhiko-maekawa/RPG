﻿using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.ValueObject
{
    public class CureSkillResultValueObject : ISkillResult
    {
        public CureSkillResultValueObject(
            CharacterId actorId,
            SkillCode skillCode,
            ImmutableList<CureResultValueObject> cureList)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            TargetIdList = cureList
                .Select(x => x.TargetId)
                .ToImmutableList();
            CureList = cureList;
        }

        public ImmutableList<CureResultValueObject> CureList { get; }
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }

        public bool Success()
        {
            return CureList
                .Any(x => x.Amount > 0);
        }
    }
}