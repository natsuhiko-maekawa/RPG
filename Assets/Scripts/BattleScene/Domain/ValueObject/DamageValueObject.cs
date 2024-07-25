using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class DamageValueObject
    {
        public int AttackNumber { get; }
        public float DamageRate { get; }
        public float HitRate { get; }
        public ImmutableList<MatAttrCode> MatAttrCode { get; }
        public DamageExpressionCode DamageExpressionCode { get; }
        public HitEvaluationCode HitEvaluationCode { get; }
        public AttacksWeakPointEvaluationCode AttacksWeakPointEvaluationCode { get; }
    }
}