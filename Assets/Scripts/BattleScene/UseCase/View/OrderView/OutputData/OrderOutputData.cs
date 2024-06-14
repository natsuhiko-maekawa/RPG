using BattleScene.Domain.Code;

namespace BattleScene.UseCase.View.OrderView.OutputData
{
    public record OrderOutputData(
        OrderOutputDataType OrderOutputDataType,
        CharacterTypeId CharacterTypeId = default,
        AilmentCode AilmentCode = default,
        SlipDamageCode SlipDamageCode = default);

    public enum OrderOutputDataType
    {
        Player,
        Enemy,
        Ailment,
        SlipDamage
    }
}