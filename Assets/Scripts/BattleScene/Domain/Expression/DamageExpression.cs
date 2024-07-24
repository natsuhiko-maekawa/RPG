using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;
using Utility.Interface;

namespace BattleScene.Domain.Expression
{
    public class DamageExpression
    {
        private readonly BodyPartDomainService _bodyPartDomainService;
        private readonly IRepository<BuffEntity, BuffId> _buffRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IRandomEx _randomEx;

        public DamageExpression(
            ICharacterRepository characterRepository,
            IRepository<BuffEntity, BuffId> buffRepository,
            IRandomEx randomEx,
            BodyPartDomainService bodyPartDomainService)
        {
            _characterRepository = characterRepository;
            _buffRepository = buffRepository;
            _randomEx = randomEx;
            _bodyPartDomainService = bodyPartDomainService;
        }

        public int Evaluate(CharacterId actorId, CharacterId targetId, AbstractDamage damage)
        {
            var actorStrength = _characterRepository.Select(actorId).Property.Strength;
            var targetVitality = _characterRepository.Select(targetId).Property.Vitality;
            var actorMatAttr = damage.GetMatAttrCode();
            var targetWeekPoint = _characterRepository.Select(targetId).GetWeakPoints();
            var attackBuffId = new BuffId(actorId, BuffCode.Attack);
            var actorBuffRate = _buffRepository.Select(attackBuffId).Rate;
            var defenceBuffId = new BuffId(targetId, BuffCode.Defence);
            var targetBuffRate = _buffRepository.Select(defenceBuffId).Rate;
            var destroyedRate = 1.0f - _bodyPartDomainService.Count(actorId, BodyPartCode.Arm) * 0.5f;
            var defenceSkillBuffId = new BuffId(targetId, BuffCode.DefenceSkill);
            var targetDefence = _buffRepository.Select(defenceSkillBuffId) == null ? 1.0f : 0.5f;
            var rate = damage.GetDamageRate();
            var weekPointRate = (int)Math.Pow(2, actorMatAttr.Intersect(targetWeekPoint).Count());
            return (int)(actorStrength * actorStrength / (float)targetVitality * weekPointRate * actorBuffRate
                / targetBuffRate * destroyedRate * targetDefence * rate * 1.5f) + _randomEx.Range(1, 3);
        }
    }
}