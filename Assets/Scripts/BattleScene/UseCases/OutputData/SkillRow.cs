using BattleScene.Domain.Code;

namespace BattleScene.UseCases.OutputData
{
    public record SkillRow(
        SkillCode SkillCode,
        int TechnicalPoint,
        bool Enabled);
}