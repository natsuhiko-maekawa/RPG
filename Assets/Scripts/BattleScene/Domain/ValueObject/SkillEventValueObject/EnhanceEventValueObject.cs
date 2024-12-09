using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObject.SkillEventValueObject
{
    public class EnhanceEventValueObject : ISkillEventValueObject // 16byte
    {
        public SkillEventCode SkillEventCode { get; }
        public EnhanceCode EnhanceCode { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }
        public IReadOnlyList<CharacterId> ActualTargetIdList => TargetIdList;
        public byte EffectTurn { get; }
        public LifetimeCode LifetimeCode { get; }

        public EnhanceEventValueObject(
            EnhanceCode enhanceCode,
            byte effectTurn,
            LifetimeCode lifetimeCode,
            IReadOnlyList<CharacterId> targetIdList)
        {
            SkillEventCode = SkillEventCode.Enhance;
            EnhanceCode = enhanceCode;
            TargetIdList = targetIdList;
            EffectTurn = effectTurn;
            LifetimeCode = lifetimeCode;
        }
    }
}