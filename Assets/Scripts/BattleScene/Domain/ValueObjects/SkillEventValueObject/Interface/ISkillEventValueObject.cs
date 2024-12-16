using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;

namespace BattleScene.Domain.ValueObjects.SkillEventValueObject.Interface
{
    /// <summary>
    /// スキルイベントを表すValueObjectに実装するインタフェース。<br/>
    /// このインタフェースを実装したValueObjectは、BattleEventEntityにおいてこのインタフェース型にキャストされ、プロパティとして保持される。
    /// </summary>
    // 上述の通り、キャストが発生するためこのインタフェースの派生型はすべて構造体ではなくクラスとして実装する。
    public interface ISkillEventValueObject
    {
        public SkillEventCode SkillEventCode { get; }
        public IReadOnlyList<CharacterEntity> TargetList { get; }
    }
}