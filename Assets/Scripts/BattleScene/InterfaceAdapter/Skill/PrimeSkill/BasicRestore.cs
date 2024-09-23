using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.PrimeSkill
{
    public class BasicRestore : AbstractRestore
    {
        public override int TechnicalPoint { get; } = 10;
    }
}