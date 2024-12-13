using BattleScene.Domain.Codes;

namespace BattleScene.InterfaceAdapter.SkillComponents.BaseClass
{
    public abstract class BaseAilment
    {
        public abstract AilmentCode AilmentCode { get; }
        public virtual float LuckRate { get; } = 0.5f;
    }
}