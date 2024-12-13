using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObjects.SkillEventValueObject
{
    public class BuffEventValueObject : ISkillEventValueObject // 16byte
    {
        public SkillEventCode SkillEventCode { get; }
        public BuffCode BuffCode { get; }
        public float Rate { get; }
        public byte EffectTurn { get; }
        public LifetimeCode LifetimeCode { get; }
        public IReadOnlyList<CharacterEntity> TargetList { get; }

        public BuffEventValueObject(
            BuffCode buffCode,
            IReadOnlyList<CharacterEntity> targetList,
            byte effectTurn,
            LifetimeCode lifetimeCode,
            float rate = 1.0f)
        {
            SkillEventCode = SkillEventCode.Buff;
            BuffCode = buffCode;
            Rate = rate;
            EffectTurn = effectTurn;
            LifetimeCode = lifetimeCode;
            TargetList = targetList;
        }
    }
}