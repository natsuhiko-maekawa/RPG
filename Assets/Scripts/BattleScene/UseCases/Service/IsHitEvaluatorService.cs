using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class IsHitEvaluatorService
    {
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;
        private readonly BodyPartDomainService _bodyPartDomainService;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly IRepository<BuffEntity, (CharacterId, BuffCode)> _buffRepository;
        private readonly IRepository<EnhanceEntity, (CharacterId, EnhanceCode)> _enhanceRepository;
        private readonly IMyRandomService _myRandom;
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;

        public IsHitEvaluatorService(
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository,
            BodyPartDomainService bodyPartDomainService,
            CharacterPropertyFactoryService characterPropertyFactory,
            IRepository<BuffEntity, (CharacterId, BuffCode)> buffRepository,
            IRepository<EnhanceEntity, (CharacterId, EnhanceCode)> enhanceRepository,
            IMyRandomService myRandom,
            IFactory<BattlePropertyValueObject> battlePropertyFactory)
        {
            _ailmentRepository = ailmentRepository;
            _bodyPartDomainService = bodyPartDomainService;
            _characterPropertyFactory = characterPropertyFactory;
            _buffRepository = buffRepository;
            _enhanceRepository = enhanceRepository;
            _myRandom = myRandom;
            _battlePropertyFactory = battlePropertyFactory;
        }

        public bool Evaluate(CharacterEntity actor, CharacterEntity target, DamageValueObject damage)
        {
            return damage.HitEvaluationCode switch
            {
                HitEvaluationCode.Basic => BasicEvaluate(actor, target, damage),
                HitEvaluationCode.AlwaysHit => AlwaysHitEvaluate(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        // ReSharper disable once InvalidXmlDocComment
        /// <summary>
        /// 命中したかどうかを判定する最も基本的なメソッド。<br/>
        /// <see cref="BattleScene.Debug.Service.DebugRandomService"/>でメソッド名を利用してリフレクションを行っているため、
        /// NOTE: メソッド名を変更するときは上記クラスも修正すること。
        /// </summary>
        /// <param name="actor">行動者のID</param>
        /// <param name="target">攻撃対象のID</param>
        /// <param name="damage">ダメージスキルの実引数</param>
        /// <returns>命中した場合、true。それ以外はfalse。</returns>
        private bool BasicEvaluate(CharacterEntity actor, CharacterEntity target, DamageValueObject damage)
        {
            // 両脚損傷時、必ず命中する
            if (!_bodyPartDomainService.IsAvailable(target.Id, BodyPartCode.Leg)) return true;

            // 空蝉状態の時、必ず回避する
            if (_enhanceRepository.TryGet((target.Id, EnhanceCode.Utsusemi), out var enhance) && enhance.Effects) 
                return false;

            // 大きいほど命中しやすくなる
            var threshold = _battlePropertyFactory.Create().IsHitThreshold;
            var actorAgility = _characterPropertyFactory.Create(actor.Id).Agility;
            var targetAgility = _characterPropertyFactory.Create(target.Id).Agility;
            var isActorBlind = _ailmentRepository.Get((actor.Id, AilmentCode.Blind)).Effects;
            var isTargetDeaf = _ailmentRepository.Get((actor.Id, AilmentCode.Deaf)).Effects;
            var destroyedReduce = _bodyPartDomainService.Count(target.Id, BodyPartCode.Leg) * 0.5f;
            var buff = (float)Math.Log(_buffRepository.Get((actor.Id, BuffCode.HitRate)).Rate, 2.0f);
            var add = (float)Math.Log(damage.HitRate, 2.0f);
            var actorFixedAgility = actorAgility + (isActorBlind ? -threshold : 0);
            var targetFixedAgility = targetAgility + (isTargetDeaf ? -threshold : 0);
            var hitRate = 1.0f + (actorFixedAgility - targetFixedAgility) / threshold;
            return _myRandom.Probability(hitRate + destroyedReduce + buff + add);
        }

        private bool AlwaysHitEvaluate() => true;
    }
}