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
        private const float MultiplicationIdentityElement = 1.0f;
        private readonly BodyPartDomainService _bodyPartDomainService;
        private readonly ICollection<BuffEntity, (CharacterId, BuffCode)> _buffCollection;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly ICollection<EnhanceEntity, (CharacterId, EnhanceCode)> _enhanceCollection;
        private readonly IMyRandomService _myRandom;

        public DamageEvaluatorService(
            BodyPartDomainService bodyPartDomainService,
            ICollection<BuffEntity, (CharacterId, BuffCode)> buffCollection,
            CharacterPropertyFactoryService characterPropertyFactoryFactory,
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            ICollection<EnhanceEntity, (CharacterId, EnhanceCode)> enhanceCollection,
            IMyRandomService myRandom)
        {
            _bodyPartDomainService = bodyPartDomainService;
            _buffCollection = buffCollection;
            _characterPropertyFactory = characterPropertyFactoryFactory;
            _battlePropertyFactory = battlePropertyFactory;
            _enhanceCollection = enhanceCollection;
            _myRandom = myRandom;
        }

        public int Evaluate(CharacterId actorId, CharacterId targetId, DamageParameterValueObject damageParameter)
        {
            return damageParameter.DamageExpressionCode switch
            {
                DamageExpressionCode.Basic => BasicEvaluate(actorId, targetId, damageParameter),
                DamageExpressionCode.Constant => ConstantEvaluate(actorId, damageParameter),
                DamageExpressionCode.Slip => SlipEvaluate(actorId, targetId),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private int BasicEvaluate(CharacterId actorId, CharacterId targetId, DamageParameterValueObject damageParameter)
        {
            var actorStrength = _characterPropertyFactory.Create(actorId).Strength;
            var targetVitality = _characterPropertyFactory.Create(targetId).Vitality;
            var actorMatAttr = damageParameter.MatAttrCode;
            var targetWeekPoint = _characterPropertyFactory.Create(targetId).WeakPointsCodeList;
            var actorBuffRate = _buffCollection.Get((actorId, BuffCode.Attack)).Rate;
            var targetBuffRate = _buffCollection.Get((targetId, BuffCode.Defence)).Rate;
            var destroyedRate = 1.0f - _bodyPartDomainService.Count(actorId, BodyPartCode.Arm) * 0.5f;
            // バフから防御、空蝉を切り離し、特殊効果として実装するまで保留
            var targetDefence
                = _enhanceCollection.TryGet((targetId, EnhanceCode.Defence), out var enhance) && enhance.Effects
                    ? 0.5f
                    : MultiplicationIdentityElement;
            var rate = damageParameter.DamageRate;
            var weekPointRate = (int)Math.Pow(2, actorMatAttr.Intersect(targetWeekPoint).Count());
            return (int)(actorStrength * actorStrength / (float)targetVitality * weekPointRate * actorBuffRate
                / targetBuffRate * destroyedRate * targetDefence * rate * 1.5f) + _myRandom.Range(1, 3);
        }

        private int ConstantEvaluate(CharacterId actorId, DamageParameterValueObject damageParameter)
        {
            var actorStrength = _characterPropertyFactory.Create(actorId).Strength;
            const int targetVitality = 1;
            var actorBuffRate = _buffCollection.Get((actorId, BuffCode.Attack)).Rate;
            const int targetBuffRate = 1;
            var destroyedRate
                = MultiplicationIdentityElement - _bodyPartDomainService.Count(actorId, BodyPartCode.Arm) * 0.5f;
            var rate = damageParameter.DamageRate;
            return (int)(actorStrength * actorStrength / (float)targetVitality * actorBuffRate / targetBuffRate
                         * destroyedRate * rate * 1.5f) + _myRandom.Range(1, 3);
        }

        private int SlipEvaluate(CharacterId actorId, CharacterId targetId)
        {
            var enemyIntelligence = _characterPropertyFactory.Create(actorId).Intelligence;
            var playerIntelligence = _characterPropertyFactory.Create(targetId).Intelligence;
            var damageRate = _battlePropertyFactory.Create().SlipDefaultDamageRate;
            var damage = (int)(enemyIntelligence * enemyIntelligence / (float)playerIntelligence * damageRate)
                         + _myRandom.Range(0, 2);
            return damage;
        }
    }
}