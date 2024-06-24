using System.Collections.Immutable;

namespace BattleScene.UseCases.View.SelectSkillView.OutputData
{
    public record SelectSkillOutputData(
        int Selection,
        int ListStart,
        int UpperLimit,
        ImmutableList<SkillInfo> SkillList);

    public record SkillInfo(
        string Name,
        int Tp,
        bool Disabled);
}