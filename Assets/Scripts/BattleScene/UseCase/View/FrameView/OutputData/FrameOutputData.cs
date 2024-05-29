namespace BattleScene.UseCase.View.FrameView.OutputData
{
    public record FrameOutputData(
        bool IsPlayer,
        int EnemyNumber,
        FrameType FrameType);

    public enum FrameType
    {
        Actor,
        Target
    }
}