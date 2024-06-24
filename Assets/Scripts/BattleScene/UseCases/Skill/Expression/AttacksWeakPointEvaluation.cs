using System.Linq;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.Expression
{
    public class AttacksWeakPointEvaluation
    {
        private readonly ICharacterRepository _characterRepository;

        public AttacksWeakPointEvaluation(
            ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public bool Evaluate(CharacterId targetId, DamageSkillElement damageSkillElement)
        {
            return _characterRepository.Select(targetId).GetWeakPoints()
                .Intersect(damageSkillElement.GetMatAttrCode())
                .Any();
        }
    }
}