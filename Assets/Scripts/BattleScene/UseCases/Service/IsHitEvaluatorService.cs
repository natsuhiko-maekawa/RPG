using System;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;
using UnityEngine;
using Utility.Interface;

namespace BattleScene.UseCases.Service
{
    public class IsHitEvaluatorService
    {
        private readonly IRepository<AilmentEntity, AilmentId> _ailmentRepository;
        private readonly BodyPartDomainService _bodyPartDomainService;
        private readonly IRepository<BuffEntity, BuffId> _buffRepository;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRandomEx _randomEx;

        public IsHitEvaluatorService(
            IRepository<AilmentEntity, AilmentId> ailmentRepository,
            BodyPartDomainService bodyPartDomainService,
            IRepository<BuffEntity, BuffId> buffRepository,
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            IRandomEx randomEx)
        {
            _ailmentRepository = ailmentRepository;
            _bodyPartDomainService = bodyPartDomainService;
            _buffRepository = buffRepository;
            _characterRepository = characterRepository;
            _randomEx = randomEx;
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

            var utsusemiSkillBuffId = new BuffId(targetId, BuffCode.UtsusemiSkill);
            if (_buffRepository.Select(utsusemiSkillBuffId) != null) return false;

            // 大きいほど命中しやすくなる
            const float threshold = 20.0f;
            var actorAgility = _characterRepository.Select(actorId).Property.Agility;
            var targetAgility = _characterRepository.Select(targetId).Property.Agility;
            var isActorBlind = _ailmentRepository.Select()
                .FirstOrDefault(x => Equals(x.CharacterId, actorId) && x.AilmentCode == AilmentCode.Blind) != null;
            var isTargetDeaf = _ailmentRepository.Select()
                .FirstOrDefault(x => Equals(x.CharacterId, targetId) && x.AilmentCode == AilmentCode.Deaf) != null;
            var destroyedReduce = _bodyPartDomainService.Count(targetId, BodyPartCode.Leg) * 0.5f;
            var hitRateBuffId = new BuffId(actorId, BuffCode.HitRate);
            var buff = Mathf.Log(_buffRepository.Select(hitRateBuffId).Rate, 2.0f);
            var add = Mathf.Log(damageParameter.HitRate, 2.0f);
            var actorFixedAgility = actorAgility + (isActorBlind ? -threshold : 0);
            var targetFixedAgility = targetAgility + (isTargetDeaf ? -threshold : 0);
            var hitRate = 1.0f + (actorFixedAgility - targetFixedAgility) / threshold;
            return _randomEx.Probability(hitRate + destroyedReduce + buff + add);
        }

        private bool AlwaysHitEvaluate()
        {
            return true;
        }
    }
}