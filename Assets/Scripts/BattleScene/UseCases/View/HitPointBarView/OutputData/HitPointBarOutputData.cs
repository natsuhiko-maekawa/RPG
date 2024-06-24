using BattleScene.UseCases.Service;

namespace BattleScene.UseCases.View.HitPointBarView.OutputData
{
    public record HitPointBarOutputData(
        CharacterOutputData CharacterOutputData,
        int MaxHitPoint,
        int CurrentHitPoint);
}