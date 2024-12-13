using System.Collections.Generic;
using BattleScene.Domain.Entities;

namespace BattleScene.Domain.ValueObjects.SkillEventValueObject.Interface
{
    public interface IFailable
    {
        public IReadOnlyList<CharacterEntity> ActualTargetList { get; }
    }
}