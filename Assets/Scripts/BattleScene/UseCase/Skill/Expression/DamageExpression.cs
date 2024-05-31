using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Skill.AbstractClass;
using Utility;
using static BattleScene.Domain.Code.BuffCode;
using static BattleScene.Domain.Code.BodyPartCode;

namespace BattleScene.UseCase.Skill.Expression
{
    public class DamageExpression
    {
        private readonly BodyPartDomainService _bodyPartDomainService;
        private readonly IBuffRepository _buffRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IRandomEx _randomEx;

        public DamageExpression(
            ICharacterRepository characterRepository,
            IBuffRepository buffRepository,
            IRandomEx randomEx,
            BodyPartDomainService bodyPartDomainService)
        {
            _characterRepository = characterRepository;
            _buffRepository = buffRepository;
            _randomEx = randomEx;
            _bodyPartDomainService = bodyPartDomainService;
        }

        public int Evaluate(CharacterId actorId, CharacterId targetId, DamageSkillElement damageSkillElement)
        {
            var actorStrength = _characterRepository.Select(actorId).Property.Strength;
            var targetVitality = _characterRepository.Select(targetId).Property.Vitality;
            var actorMatAttr = damageSkillElement.GetMatAttrCode();
            var targetWeekPoint = _characterRepository.Select(targetId).GetWeakPoints();
            var actorBuffRate = _buffRepository.Select(actorId, Attack).Rate;
            var targetBuffRate = _buffRepository.Select(targetId, Defence).Rate;
            var destroyedRate = 1.0f - _bodyPartDomainService.Count(actorId, Arm) * 0.5f;
            var targetDefence = _buffRepository.Select(targetId, BuffCode.DefenceSkill) == null ? 1.0f : 0.5f;
            var rate = damageSkillElement.GetDamageRate();
            var weekPointRate = (int)Math.Pow(2, actorMatAttr.Intersect(targetWeekPoint).Count());
            return (int)(actorStrength * actorStrength / (float)targetVitality * weekPointRate * actorBuffRate
                / targetBuffRate * destroyedRate * targetDefence * rate * 1.5f) + _randomEx.Range(1, 3);
        }
    }
}