using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObjects.SkillEventValueObject
{
    public class EnhanceEventValueObject : ISkillEventValueObject // 16byte
    {
        public SkillEventCode SkillEventCode { get; }
        public EnhanceCode EnhanceCode { get; }
        public IReadOnlyList<CharacterEntity> TargetList { get; }
        public byte EffectTurn { get; }
        public LifetimeCode LifetimeCode { get; }

        public EnhanceEventValueObject(
            EnhanceCode enhanceCode,
            byte effectTurn,
            LifetimeCode lifetimeCode,
            IReadOnlyList<CharacterEntity> targetList)
        {
            SkillEventCode = SkillEventCode.Enhance;
            EnhanceCode = enhanceCode;
            TargetList = targetList;
            EffectTurn = effectTurn;
            LifetimeCode = lifetimeCode;
        }
    }
}