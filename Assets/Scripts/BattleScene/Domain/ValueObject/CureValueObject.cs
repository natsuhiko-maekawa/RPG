﻿using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class CureValueObject
    {
        public CureValueObject(
            CharacterId actorId,
            SkillCode skillCode,
            ImmutableList<RecoveryValueObject> cureList)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            TargetIdList = cureList
                .Select(x => x.TargetId)
                .ToImmutableList();
            CureList = cureList;
        }

        public ImmutableList<RecoveryValueObject> CureList { get; }
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