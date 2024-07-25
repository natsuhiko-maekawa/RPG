using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record DamageValueObject(
        int AttackNumber,
        float DamageRate,
        float HitRate,
        ImmutableList<MatAttrCode> MatAttrCode,
        DamageExpressionCode DamageExpressionCode,
        HitEvaluationCode HitEvaluationCode,
        AttacksWeakPointEvaluationCode AttacksWeakPointEvaluationCode
    );
}