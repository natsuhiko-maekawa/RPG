using System.Collections.Generic;
using BattleScene.Domain.Entity;

namespace BattleScene.Domain.ValueObject.SkillEventValueObject.Interface
{
    public interface IFailable
    {
        public IReadOnlyList<CharacterEntity> ActualTargetList { get; }
    }
}