using BattleScene.Domain.Code;

namespace BattleScene.Domain.Interface
{
    public interface IAilmentSkill : ISkillElement, ILuckSkillElement
    {
        public AilmentCode GetAilmentsCode();
    }
}