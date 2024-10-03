using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class DamageEvaluatorService
    {
        private readonly BodyPartDomainService _bodyPartDomainService;
        private readonly BuffDomainService _buff;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly IRepository<BuffEntity, (CharacterId, BuffCode)> _buffRepository;
        private readonly IMyRandomService _myRandom;

        public DamageEvaluatorService(
            BodyPartDomainService bodyPartDomainService,
            BuffDomainService buff,
            CharacterPropertyFactoryService characterPropertyFactoryFactory,
            IRepository<BuffEntity, (CharacterId, BuffCode)> buffRepository, 
            IMyRandomService myRandom)
        {
            _bodyPartDomainService = bodyPartDomainService;
            _buff = buff;
            _characterPropertyFactory = characterPropertyFactoryFactory;
            _buffRepository = buffRepository;
            _myRandom = myRandom;
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
            var actorStrength = _characterPropertyFactory.Crate(actorId).Strength;
            var targetVitality = _characterPropertyFactory.Crate(targetId).Vitality;
            var actorMatAttr = damageParameter.MatAttrCode;
            var targetWeekPoint = _characterPropertyFactory.Crate(targetId).WeakPoints;
            var actorBuffRate = _buff.GetRate(actorId, BuffCode.Attack);
            var targetBuffRate = _buff.GetRate(targetId, BuffCode.Defence);
            var destroyedRate = 1.0f - _bodyPartDomainService.Count(actorId, BodyPartCode.Arm) * 0.5f;
            var targetDefence = _buffRepository.Select((targetId, BuffCode.DefenceSkill)) == null ? 1.0f : 0.5f;
            var rate = damageParameter.DamageRate;
            var weekPointRate = (int)Math.Pow(2, actorMatAttr.Intersect(targetWeekPoint).Count());
            return (int)(actorStrength * actorStrength / (float)targetVitality * weekPointRate * actorBuffRate
                / targetBuffRate * destroyedRate * targetDefence * rate * 1.5f) + _myRandom.Range(1, 3);
        }
        
        private int ConstantEvaluate(CharacterId actorId, DamageParameterValueObject damageParameter)
        {
            var actorStrength = _characterPropertyFactory.Crate(actorId).Strength;
            const int targetVitality = 1;
            var actorBuffRate = _buff.GetRate(actorId, BuffCode.Attack);
            const int targetBuffRate = 1;
            var destroyedRate = 1.0f - _bodyPartDomainService.Count(actorId, BodyPartCode.Arm) * 0.5f;
            var rate = damageParameter.DamageRate;
            return (int)(actorStrength * actorStrength / (float)targetVitality * actorBuffRate / targetBuffRate
                         * destroyedRate * rate * 1.5f) + _myRandom.Range(1, 3);
        }
    }
}