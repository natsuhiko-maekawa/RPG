using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.Skill.AbstractClass;
using UnityEngine;
using Utility;
using static BattleScene.Domain.Code.AilmentCode;
using static BattleScene.Domain.Code.BuffCode;
using static BattleScene.Domain.Code.BodyPartCode;

namespace BattleScene.UseCase.Skill.Expression
{
    public class HitEvaluation
    {
        private readonly IAilmentRepository _ailmentRepository;
        private readonly BodyPartDomainService _bodyPartDomainService;
        private readonly IBuffRepository _buffRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IRandomEx _randomEx;

        public HitEvaluation(
            IAilmentRepository ailmentRepository,
            IBuffRepository buffRepository,
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

        public bool Evaluate(CharacterId actorId, CharacterId targetId, DamageSkillElement damageSkillElement)
        {
            // 両脚損傷時、必ず命中する
            if (!_bodyPartDomainService.IsAvailable(targetId, Leg)) return true;

            if (_buffRepository.Select(targetId, BuffCode.UtsusemiSkill) != null) return false;

            // 大きいほど命中しやすくなる
            const float threshold = 20.0f;
            var actorAgility = _characterRepository.Select(actorId).Property.Agility;
            var targetAgility = _characterRepository.Select(targetId).Property.Agility;
            var isActorBlind = _ailmentRepository.Select(actorId, Blind) != null;
            var isTargetDeaf = _ailmentRepository.Select(targetId, Deaf) != null;
            var destroyedReduce = _bodyPartDomainService.Count(targetId, Leg) * 0.5f;
            var buff = Mathf.Log(_buffRepository.Select(actorId, HitRate).Rate, 2.0f);
            var add = Mathf.Log(damageSkillElement.GetHitRate(), 2.0f);
            var actorFixedAgility = actorAgility + (isActorBlind ? -threshold : 0);
            var targetFixedAgility = targetAgility + (isTargetDeaf ? -threshold : 0);
            var hitRate = 1.0f + (actorFixedAgility - targetFixedAgility) / threshold;
            return _randomEx.Probability(hitRate + destroyedReduce + buff + add);
        }
    }
}