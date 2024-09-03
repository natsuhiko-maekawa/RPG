using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class BasicRestore : AbstractRestore
    {
        public override int TechnicalPoint { get; } = 10;
    }
}