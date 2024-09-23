using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public record SlipValueObject(
        CharacterId ActorId,
        SkillCode SkillCode,
        SlipDamageCode SlipDamageCode,
        IReadOnlyList<CharacterId> TargetIdList,
        IReadOnlyList<CharacterId> ActualTargetIdList);
}