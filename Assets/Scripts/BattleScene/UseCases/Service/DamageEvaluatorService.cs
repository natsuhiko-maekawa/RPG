using System;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using Utility.Interface;

namespace BattleScene.UseCases.Service
{
    public class DamageEvaluatorService
    {
        private readonly BodyPartDomainService _bodyPartDomainService;
        private readonly BuffDomainService _buffDomainService;
        private readonly IRepository<BuffEntity, BuffId> _buffRepository;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRandomEx _randomEx;

        public DamageEvaluatorService(
            BodyPartDomainService bodyPartDomainService,
            BuffDomainService buffDomainService,
            IRepository<BuffEntity, BuffId> buffRepository, 
            IRepository<CharacterAggregate, CharacterId> characterRepository, 
            IRandomEx randomEx)
        {
            _bodyPartDomainService = bodyPartDomainService;
            _buffDomainService = buffDomainService;
            _buffRepository = buffRepository;
            _characterRepository = characterRepository;
            _randomEx = randomEx;
        }

        public int Evaluate(CharacterId actorId, CharacterId targetId, DamageParameterValueObject damageParameter)
        {
            return damageParameter.DamageExpressionCode switch
            {
                DamageExpressionCode.Basic => BasicEvaluate(actorId, targetId, damageParameter),
                DamageExpressionCode.Constant => ConstantEvaluate(actorId, damageParameter),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private int BasicEvaluate(CharacterId actorId, CharacterId targetId, DamageParameterValueObject damageParameter)
        {
            var actorStrength = _characterRepository.Select(actorId).Property.Strength;
            var targetVitality = _characterRepository.Select(targetId).Property.Vitality;
            var actorMatAttr = damageParameter.MatAttrCode;
            var targetWeekPoint = _characterRepository.Select(targetId).GetWeakPoints();
            var attackBuffId = new BuffId(actorId, BuffCode.Attack);
            var actorBuffRate = _buffRepository.Select(attackBuffId)?.Rate ?? 1;
            var defenceBuffId = new BuffId(targetId, BuffCode.Defence);
            var targetBuffRate = _buffRepository.Select(defenceBuffId)?.Rate ?? 1;
            var destroyedRate = 1.0f - _bodyPartDomainService.Count(actorId, BodyPartCode.Arm) * 0.5f;
            var defenceSkillBuffId = new BuffId(targetId, BuffCode.DefenceSkill);
            var targetDefence = _buffRepository.Select(defenceSkillBuffId) == null ? 1.0f : 0.5f;
            var rate = damageParameter.DamageRate;
            var weekPointRate = (int)Math.Pow(2, actorMatAttr.Intersect(targetWeekPoint).Count());
            return (int)(actorStrength * actorStrength / (float)targetVitality * weekPointRate * actorBuffRate
                / targetBuffRate * destroyedRate * targetDefence * rate * 1.5f) + _randomEx.Range(1, 3);
        }
        
        private int ConstantEvaluate(CharacterId actorId, DamageParameterValueObject damageParameter)
        {
            var actorStrength = _characterRepository.Select(actorId).Property.Strength;
            const int targetVitality = 1;
            var actorBuffRate = _buffDomainService.GetRate(actorId, BuffCode.Attack);
            const int targetBuffRate = 1;
            var destroyedRate = 1.0f - _bodyPartDomainService.Count(actorId, BodyPartCode.Arm) * 0.5f;
            var rate = damageParameter.DamageRate;
            return (int)(actorStrength * actorStrength / (float)targetVitality * actorBuffRate / targetBuffRate
                         * destroyedRate * rate * 1.5f) + _randomEx.Range(1, 3);
        }
    }
}