﻿using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.ValueObject
{
    public class SkillValueObject
    {
        [Obsolete]
        public SkillCode SkillCode { get; }
        public int TechnicalPoint { get; }
        public ImmutableList<BodyPartCode> DependencyList { get; }
        public Range Range { get; }
        public SkillCommonValueObject SkillCommon { get; }
        // TODO: 命名をやり直す
        public ImmutableList<AilmentValueObject> AilmentList { get; }
        public ImmutableList<BuffParameterValueObject> BuffList { get; }
        public ImmutableList<DamageParameterValueObject> DamageList { get; }
        public ImmutableList<DestroyedPartParameterValueObject> DestroyedPartParameterList { get; }
        public ImmutableList<RestoreParameterValueObject> RestoreParameterList { get; }

        public SkillValueObject(
            SkillCode skillCode,
            Range range,
            MessageCode messageCode,
            int technicalPoint = 0,
            ImmutableList<BodyPartCode> dependencyList = null,
            ImmutableList<AilmentValueObject> ailmentList = null,
            ImmutableList<BuffParameterValueObject> buffList = null,
            ImmutableList<DamageParameterValueObject> damageList = null,
            ImmutableList<DestroyedPartParameterValueObject> destroyedPartList = null,
            ImmutableList<RestoreParameterValueObject> restoreParameterList = null)
        {
            SkillCode = skillCode;
            Range = range;
            TechnicalPoint = technicalPoint;
            DependencyList = dependencyList ?? ImmutableList<BodyPartCode>.Empty;
            SkillCommon = new SkillCommonValueObject(
                skillCode: skillCode,
                technicalPoint: technicalPoint,
                dependencyList: dependencyList,
                range: range,
                messageCode: messageCode);
            AilmentList = ailmentList ?? ImmutableList<AilmentValueObject>.Empty;
            BuffList = buffList ?? ImmutableList<BuffParameterValueObject>.Empty;
            DamageList = damageList ?? ImmutableList<DamageParameterValueObject>.Empty;
            DestroyedPartParameterList = destroyedPartList ?? ImmutableList<DestroyedPartParameterValueObject>.Empty;
            RestoreParameterList = restoreParameterList ?? ImmutableList<RestoreParameterValueObject>.Empty;
        }
    }
}