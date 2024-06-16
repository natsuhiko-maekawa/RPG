using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Skill.SkillElement.AbstractClass;
using Utility.Interface;
using static BattleScene.Domain.Code.BuffCode;
using static BattleScene.Domain.Code.BodyPartCode;

namespace BattleScene.UseCase.Skill.Expression
{
    public class ConstantDamageExpression
    {
        private readonly BodyPartDomainService _bodyPartDomainService;
        private readonly BuffDomainService _buffDomainService;
        private readonly IBuffRepository _buffRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IRandomEx _randomEx;

        public ConstantDamageExpression(
            ICharacterRepository characterRepository,
            IRandomEx randomEx,
            BodyPartDomainService bodyPartDomainService,
            BuffDomainService buffDomainService)
        {
            _characterRepository = characterRepository;
            _randomEx = randomEx;
            _bodyPartDomainService = bodyPartDomainService;
            _buffDomainService = buffDomainService;
        }

        public int Evaluate(CharacterId actorId, DamageSkillElement damageSkillElement)
        {
            var actorStrength = _characterRepository.Select(actorId).Property.Strength;
            var targetVitality = 1;
            var actorBuffRate = _buffDomainService.GetRate(actorId, Attack);
            var targetBuffRate = 1;
            var destroyedRate = 1.0f - _bodyPartDomainService.Count(actorId, Arm) * 0.5f;
            var rate = damageSkillElement.GetDamageRate();
            return (int)(actorStrength * actorStrength / (float)targetVitality * actorBuffRate / targetBuffRate
                         * destroyedRate * rate * 1.5f) + _randomEx.Range(1, 3);
        }
    }
}