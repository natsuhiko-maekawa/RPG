using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.ValueObject
{
    public record DamageParameterValueObject(
        int AttackNumber,
        float DamageRate,
        float HitRate,
        ImmutableList<MatAttrCode> MatAttrCode,
        DamageExpressionCode DamageExpressionCode,
        HitEvaluationCode HitEvaluationCode,
        AttacksWeakPointEvaluationCode AttacksWeakPointEvaluationCode
    ) : ISkillEffect;
}