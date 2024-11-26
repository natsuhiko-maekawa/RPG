namespace BattleScene.Framework.ViewModel
{
    public record RowDto(
        int RowId,
        string RowName,
        string[] RowDescription,
        string PlayerImagePath,
        bool Enabled,
        int TechnicalPoint = 0);
}