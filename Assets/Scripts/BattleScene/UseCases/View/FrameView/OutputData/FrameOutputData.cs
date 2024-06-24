namespace BattleScene.UseCases.View.FrameView.OutputData
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