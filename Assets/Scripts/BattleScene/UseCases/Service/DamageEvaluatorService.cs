using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class DamageEvaluatorService
    {
        private const float MultiplicationIdentityElement = 1.0f;
        private readonly BodyPartDomainService _bodyPartDomainService;
        private readonly IRepository<BuffEntity, (CharacterId, BuffCode)> _buffRepository;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly IRepository<EnhanceEntity, (CharacterId, EnhanceCode)> _enhanceRepository;
        private readonly IMyRandomService _myRandom;

        public DamageEvaluatorService(
            BodyPartDomainService bodyPartDomainService,
            IRepository<BuffEntity, (CharacterId, BuffCode)> buffRepository,
            CharacterPropertyFactoryService characterPropertyFactoryFactory,
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            IRepository<EnhanceEntity, (CharacterId, EnhanceCode)> enhanceRepository,
            IMyRandomService myRandom)
        {
            _bodyPartDomainService = bodyPartDomainService;
            _buffRepository = buffRepository;
            _characterPropertyFactory = characterPropertyFactoryFactory;
            _battlePropertyFactory = battlePropertyFactory;
            _enhanceRepository = enhanceRepository;
            _myRandom = myRandom;
        }

        public int Evaluate(CharacterEntity actor, CharacterEntity target, DamageValueObject damage)
        {
            return damage.DamageExpressionCode switch
            {
                DamageExpressionCode.Basic => BasicEvaluate(actor, target, damage),
                DamageExpressionCode.Constant => ConstantEvaluate(actor, damage),
                DamageExpressionCode.Slip => SlipEvaluate(actor, target),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private int BasicEvaluate(CharacterEntity actor, CharacterEntity target, DamageValueObject damage)
        {
            var actorStrength = _characterPropertyFactory.Create(actor.Id).Strength;
            var targetVitality = _characterPropertyFactory.Create(target.Id).Vitality;
            var actorMatAttr = damage.MatAttrCode;
            var targetWeekPoint = _characterPropertyFactory.Create(target.Id).WeakPointsCode;
            var actorBuffRate = _buffRepository.Get((actor.Id, BuffCode.Attack)).Rate;
            var targetBuffRate = _buffRepository.Get((target.Id, BuffCode.Defence)).Rate;
            var destroyedRate = 1.0f - _bodyPartDomainService.Count(actor.Id, BodyPartCode.Arm) * 0.5f;
            var targetDefence
                = _enhanceRepository.TryGet((target.Id, EnhanceCode.Defence), out var enhance) && enhance.Effects
                    ? 0.5f
                    : MultiplicationIdentityElement;
            var rate = damage.DamageRate;
            var matchedWeekPointCount = BitUtility.BitCount((uint)(actorMatAttr & targetWeekPoint));
            var weekPointRate = (int)Math.Pow(2, matchedWeekPointCount);
            return (int)(actorStrength * actorStrength / (float)targetVitality * weekPointRate * actorBuffRate
                / targetBuffRate * destroyedRate * targetDefence * rate * 1.5f) + _myRandom.Range(1, 3);
        }

        private int ConstantEvaluate(CharacterEntity actor, DamageValueObject damage)
        {
            var actorStrength = _characterPropertyFactory.Create(actor.Id).Strength;
            const int targetVitality = 1;
            var actorBuffRate = _buffRepository.Get((actor.Id, BuffCode.Attack)).Rate;
            const int targetBuffRate = 1;
            var destroyedRate
                = MultiplicationIdentityElement - _bodyPartDomainService.Count(actor.Id, BodyPartCode.Arm) * 0.5f;
            var rate = damage.DamageRate;
            return (int)(actorStrength * actorStrength / (float)targetVitality * actorBuffRate / targetBuffRate
                         * destroyedRate * rate * 1.5f) + _myRandom.Range(1, 3);
        }

        private int SlipEvaluate(CharacterEntity actor, CharacterEntity target)
        {
            var enemyIntelligence = _characterPropertyFactory.Create(actor.Id).Intelligence;
            var playerIntelligence = _characterPropertyFactory.Create(target.Id).Intelligence;
            var damageRate = _battlePropertyFactory.Create().SlipDefaultDamageRate;
            var damage = (int)(enemyIntelligence * enemyIntelligence / (float)playerIntelligence * damageRate)
                         + _myRandom.Range(0, 2);
            return damage;
        }
    }
}