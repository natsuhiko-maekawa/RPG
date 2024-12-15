using BattleScene.Domain.Codes;

namespace BattleScene.Domain.ValueObjects
{
    public struct DamageValueObject // 16 byte
    {
        public int AttackCount { get; }
        public float DamageRate { get; }
        public float HitRate { get; }
        public MatAttrCode MatAttrCode { get; }
        public DamageExpressionCode DamageExpressionCode { get; }
        public HitEvaluationCode HitEvaluationCode { get; }
        public AttacksWeakPointEvaluationCode AttacksWeakPointEvaluationCode { get; }

        public DamageValueObject(
            int attackCount = 1,
            float damageRate = 1.0f,
            float hitRate = 1.0f,
            MatAttrCode matAttrCode = MatAttrCode.NoAttr,
            DamageExpressionCode damageExpressionCode = DamageExpressionCode.Basic,
            HitEvaluationCode hitEvaluationCode = HitEvaluationCode.Basic,
            AttacksWeakPointEvaluationCode attacksWeakPointEvaluationCode = AttacksWeakPointEvaluationCode.Basic)
        {
            AttackCount = attackCount;
            DamageRate = damageRate;
            HitRate = hitRate;
            MatAttrCode = matAttrCode;
            DamageExpressionCode = damageExpressionCode;
            HitEvaluationCode = hitEvaluationCode;
            AttacksWeakPointEvaluationCode = attacksWeakPointEvaluationCode;
        }
    }
}