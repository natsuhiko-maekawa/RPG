namespace BattleScene.InterfaceAdapter.Presenter.Dto
{
    public record RowDto(
        int RowId,
        string RowName,
        string RowDescription,
        string PlayerImagePath,
        bool Enabled);
}