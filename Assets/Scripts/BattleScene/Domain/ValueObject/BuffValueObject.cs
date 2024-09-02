using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.ValueObject
{
    public class BuffValueObject : ISkillResult
    {
        public BuffValueObject(
            CharacterId actorId,
            IList<CharacterId> targetIdList,
            SkillCode skillCode,
            BuffCode buffCode,
            float rate,
            int turn,
            LifetimeCode lifetimeCode)
        {
            ActorId = actorId;
            TargetIdList = targetIdList.ToImmutableList();
            SkillCode = skillCode;
            BuffCode = buffCode;
            Rate = rate;
            Turn = turn;
            LifetimeCode = lifetimeCode;
        }

        public CharacterId ActorId { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }
        public SkillCode SkillCode { get; }
        public BuffCode BuffCode { get; }
        public float Rate { get; }
        public int Turn { get; }
        public LifetimeCode LifetimeCode { get; }

        [Obsolete]
        public bool Success()
        {
            return !TargetIdList.IsEmpty;
        }
    }
}