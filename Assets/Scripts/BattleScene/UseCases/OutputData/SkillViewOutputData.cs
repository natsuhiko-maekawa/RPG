using System.Collections.Immutable;

namespace BattleScene.UseCases.OutputData
{
    public record SkillViewOutputData(
        ImmutableList<SkillRow> Row);
}