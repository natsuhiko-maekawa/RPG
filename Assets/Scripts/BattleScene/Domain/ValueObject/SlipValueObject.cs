using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public record SlipValueObject(
        CharacterId ActorId,
        SkillCode SkillCode,
        SlipDamageCode SlipDamageCode,
        ImmutableList<CharacterId> TargetIdList,
        ImmutableList<CharacterId> ActualTargetIdList);
}