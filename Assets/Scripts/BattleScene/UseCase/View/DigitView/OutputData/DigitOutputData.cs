namespace BattleScene.UseCase.View.DigitView.OutputData
{
    public record DigitOutputData(
        int Index,
        int Digit,
        bool IsAvoid,
        DigitType DigitType,
        bool IsPlayer,
        int EnemyNumber);

    public enum DigitType
    {
        DamageHp,
        RestoreHp,
        RestoreTp
    }
}