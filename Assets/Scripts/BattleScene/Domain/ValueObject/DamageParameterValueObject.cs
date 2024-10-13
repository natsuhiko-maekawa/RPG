using System.Collections.Generic;
using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public record DamageParameterValueObject(
        int AttackNumber = 1,
        float DamageRate = 1.0f,
        float HitRate = 1.0f,
        IReadOnlyList<MatAttrCode> MatAttrCode = null,
        DamageExpressionCode DamageExpressionCode = DamageExpressionCode.Basic,
        HitEvaluationCode HitEvaluationCode = HitEvaluationCode.Basic,
        AttacksWeakPointEvaluationCode AttacksWeakPointEvaluationCode = AttacksWeakPointEvaluationCode.Basic
    )
    {
        public IReadOnlyList<MatAttrCode> MatAttrCode { get; private set; } = MatAttrCode ?? new List<MatAttrCode>();
    }
}