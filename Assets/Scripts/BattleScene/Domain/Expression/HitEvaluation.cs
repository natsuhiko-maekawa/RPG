using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using UnityEngine;
using Utility.Interface;

namespace BattleScene.Domain.Expression
{
    public class HitEvaluation
    {
        private readonly IAilmentRepository _ailmentRepository;
        private readonly BodyPartDomainService _bodyPartDomainService;
        private readonly IRepository<BuffEntity, BuffId> _buffRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IRandomEx _randomEx;

        public HitEvaluation(
            IAilmentRepository ailmentRepository,
            IRepository<BuffEntity, BuffId> buffRepository,
            ICharacterRepository characterRepository,
            BodyPartDomainService bodyPartDomainService,
            IRandomEx randomEx)
        {
            _ailmentRepository = ailmentRepository;
            _buffRepository = buffRepository;
            _characterRepository = characterRepository;
            _bodyPartDomainService = bodyPartDomainService;
            _randomEx = randomEx;
        }

        public bool Evaluate(CharacterId actorId, CharacterId targetId, AbstractDamage damage)
        {
            // 両脚損傷時、必ず命中する
            if (!_bodyPartDomainService.IsAvailable(targetId, BodyPartCode.Leg)) return true;

            var utsusemiSkillBuffId = new BuffId(targetId, BuffCode.UtsusemiSkill);
            if (_buffRepository.Select(utsusemiSkillBuffId) != null) return false;

            // 大きいほど命中しやすくなる
            const float threshold = 20.0f;
            var actorAgility = _characterRepository.Select(actorId).Property.Agility;
            var targetAgility = _characterRepository.Select(targetId).Property.Agility;
            var isActorBlind = _ailmentRepository.Select(actorId, AilmentCode.Blind) != null;
            var isTargetDeaf = _ailmentRepository.Select(targetId, AilmentCode.Deaf) != null;
            var destroyedReduce = _bodyPartDomainService.Count(targetId, BodyPartCode.Leg) * 0.5f;
            var hitRateBuffId = new BuffId(actorId, BuffCode.HitRate);
            var buff = Mathf.Log(_buffRepository.Select(hitRateBuffId).Rate, 2.0f);
            var add = Mathf.Log(damage.GetHitRate(), 2.0f);
            var actorFixedAgility = actorAgility + (isActorBlind ? -threshold : 0);
            var targetFixedAgility = targetAgility + (isTargetDeaf ? -threshold : 0);
            var hitRate = 1.0f + (actorFixedAgility - targetFixedAgility) / threshold;
            return _randomEx.Probability(hitRate + destroyedReduce + buff + add);
        }
    }
}