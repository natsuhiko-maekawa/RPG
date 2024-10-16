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
        private readonly ICollection<AilmentEntity, (CharacterId, AilmentCode)> _ailmentCollection;
        private readonly BodyPartDomainService _bodyPartDomainService;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly ICollection<BuffEntity, (CharacterId, BuffCode)> _buffCollection;
        private readonly IMyRandomService _myRandom;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;

        public IsHitEvaluatorService(
            ICollection<AilmentEntity, (CharacterId, AilmentCode)> ailmentCollection,
            BodyPartDomainService bodyPartDomainService,
            CharacterPropertyFactoryService characterPropertyFactory,
            ICollection<BuffEntity, (CharacterId, BuffCode)> buffCollection,
            IMyRandomService myRandom,
            IFactory<BattlePropertyValueObject> battlePropertyFactory)
        {
            _ailmentCollection = ailmentCollection;
            _bodyPartDomainService = bodyPartDomainService;
            _characterPropertyFactory = characterPropertyFactory;
            _buffCollection = buffCollection;
            _myRandom = myRandom;
            _battlePropertyFactory = battlePropertyFactory;
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

        /// <summary>
        /// 命中したかどうかを判定する最も基本的なメソッド。<br/>
        /// <see cref="BattleScene.Debug.Service.DebugRandomService"/>でメソッド名を利用してリフレクションを行っているため、
        /// NOTE: メソッド名を変更するときは上記クラスも修正すること。
        /// </summary>
        /// <param name="actorId">行動者のID</param>
        /// <param name="targetId">攻撃対象のID</param>
        /// <param name="damageParameter">ダメージスキルの実引数</param>
        /// <returns>命中した場合、true。それ以外はfalse。</returns>
        private bool BasicEvaluate(
            CharacterId actorId,
            CharacterId targetId,
            DamageParameterValueObject damageParameter)
        {
            // 両脚損傷時、必ず命中する
            if (!_bodyPartDomainService.IsAvailable(targetId, BodyPartCode.Leg)) return true;
            
            // 空蝉状態の時、必ず回避する
            if (_buffCollection.Get((targetId, BuffCode.UtsusemiSkill)) != null) return false;

            // 大きいほど命中しやすくなる
            var threshold = _battlePropertyFactory.Create().IsHitThreshold; 
            var actorAgility = _characterPropertyFactory.Create(actorId).Agility;
            var targetAgility = _characterPropertyFactory.Create(targetId).Agility;
            var isActorBlind = _ailmentCollection.Get()
                .FirstOrDefault(x => Equals(x.CharacterId, actorId) && x.AilmentCode == AilmentCode.Blind) != null;
            var isTargetDeaf = _ailmentCollection.Get()
                .FirstOrDefault(x => Equals(x.CharacterId, targetId) && x.AilmentCode == AilmentCode.Deaf) != null;
            var destroyedReduce = _bodyPartDomainService.Count(targetId, BodyPartCode.Leg) * 0.5f;
            var buff = Mathf.Log(_buffCollection.Get()
                .FirstOrDefault(x => Equals(x.CharacterId, actorId) && x.BuffCode == BuffCode.HitRate)?.Rate ?? 1, 2.0f);
            var add = Mathf.Log(damageParameter.HitRate, 2.0f);
            var actorFixedAgility = actorAgility + (isActorBlind ? -threshold : 0);
            var targetFixedAgility = targetAgility + (isTargetDeaf ? -threshold : 0);
            var hitRate = 1.0f + (actorFixedAgility - targetFixedAgility) / threshold;
            return _myRandom.Probability(hitRate + destroyedReduce + buff + add);
        }

        private bool AlwaysHitEvaluate() => true;
    }
}