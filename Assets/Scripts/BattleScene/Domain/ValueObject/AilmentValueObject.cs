using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.ValueObject
{
    public record AilmentValueObject(
        AilmentCode AilmentCode,
        float LuckRate);
}