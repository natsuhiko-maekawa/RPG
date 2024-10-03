using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using UnityEngine;

namespace BattleScene.UseCases.Service
{
    public class IsHitEvaluatorService
    {
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;
        private readonly BodyPartDomainService _bodyPartDomainService;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly IRepository<BuffEntity, (CharacterId, BuffCode)> _buffRepository;
        private readonly IMyRandomService _myRandom;

        public IsHitEvaluatorService(
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository,
            BodyPartDomainService bodyPartDomainService,
            CharacterPropertyFactoryService characterPropertyFactory,
            IRepository<BuffEntity, (CharacterId, BuffCode)> buffRepository,
            IMyRandomService myRandom)
        {
            _ailmentRepository = ailmentRepository;
            _bodyPartDomainService = bodyPartDomainService;
            _characterPropertyFactory = characterPropertyFactory;
            _buffRepository = buffRepository;
            _myRandom = myRandom;
        }

        public bool Evaluate(CharacterId actorId, CharacterId targetId, DamageParameterValueObject damageParameter)
        {
            return damageParameter.HitEvaluationCode switch
            {
                HitEvaluationCode.Basic => BasicEvaluate(actorId, targetId, damageParameter),
                HitEvaluationCode.AlwaysHit => AlwaysHitEvaluate(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool BasicEvaluate(
            CharacterId actorId,
            CharacterId targetId,
            DamageParameterValueObject damageParameter)
        {
            // 両脚損傷時、必ず命中する
            if (!_bodyPartDomainService.IsAvailable(targetId, BodyPartCode.Leg)) return true;
            
            if (_buffRepository.Select((targetId, BuffCode.UtsusemiSkill)) != null) return false;

            // 大きいほど命中しやすくなる
            const float threshold = 20.0f;
            var actorAgility = _characterPropertyFactory.Crate(actorId).Agility;
            var targetAgility = _characterPropertyFactory.Crate(targetId).Agility;
            var isActorBlind = _ailmentRepository.Select()
                .FirstOrDefault(x => Equals(x.CharacterId, actorId) && x.AilmentCode == AilmentCode.Blind) != null;
            var isTargetDeaf = _ailmentRepository.Select()
                .FirstOrDefault(x => Equals(x.CharacterId, targetId) && x.AilmentCode == AilmentCode.Deaf) != null;
            var destroyedReduce = _bodyPartDomainService.Count(targetId, BodyPartCode.Leg) * 0.5f;
            var buff = Mathf.Log(_buffRepository.Select()
                .FirstOrDefault(x => Equals(x.CharacterId, actorId) && x.BuffCode == BuffCode.HitRate)?.Rate ?? 1, 2.0f);
            var add = Mathf.Log(damageParameter.HitRate, 2.0f);
            var actorFixedAgility = actorAgility + (isActorBlind ? -threshold : 0);
            var targetFixedAgility = targetAgility + (isTargetDeaf ? -threshold : 0);
            var hitRate = 1.0f + (actorFixedAgility - targetFixedAgility) / threshold;
            return _myRandom.Probability(hitRate + destroyedReduce + buff + add);
        }

        private bool AlwaysHitEvaluate()
        {
            return true;
        }
    }
}