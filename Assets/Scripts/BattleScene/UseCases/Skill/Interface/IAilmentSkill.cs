using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.UseCases.Skill.Interface
{
    public interface IAilmentSkill : ISkillElement, ILuckSkillElement
    {
        public AilmentCode GetAilmentsCode();
    }
}