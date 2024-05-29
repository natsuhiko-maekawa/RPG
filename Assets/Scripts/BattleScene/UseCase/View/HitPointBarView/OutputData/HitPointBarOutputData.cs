using BattleScene.UseCase.Service;

namespace BattleScene.UseCase.View.HitPointBarView.OutputData
{
    public record HitPointBarOutputData(
        CharacterOutputData CharacterOutputData,
        int MaxHitPoint,
        int CurrentHitPoint);
}