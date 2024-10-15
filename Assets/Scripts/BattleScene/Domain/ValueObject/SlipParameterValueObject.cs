﻿using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record SlipParameterValueObject(
        SlipCode SlipCode,
        float DamageRate,
        DamageExpressionCode DamageExpressionCode,
        float LuckRate);
}