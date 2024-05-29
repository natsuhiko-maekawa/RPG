namespace BattleScene.UseCase.View.OrderView.OutputData
{
    public record OrderOutputData(
        OrderOutputDataType OrderOutputDataType,
        string ImagePath,
        int AilmentNumber);

    public enum OrderOutputDataType
    {
        Player,
        Enemy,
        Ailment
    }
}