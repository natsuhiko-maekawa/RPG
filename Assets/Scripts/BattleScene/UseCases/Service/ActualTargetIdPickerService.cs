using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class ActualTargetIdPickerService
    {
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly IMyRandomService _myRandom;

        public ActualTargetIdPickerService(
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            CharacterPropertyFactoryService characterPropertyFactory,
            OrderedItemsDomainService orderedItems,
            IMyRandomService myRandom)
        {
            _battlePropertyFactory = battlePropertyFactory;
            _characterPropertyFactory = characterPropertyFactory;
            _orderedItems = orderedItems;
            _myRandom = myRandom;
        }

        /// <summary>
        /// 攻撃対象から実際の攻撃対象をランダムに絞り込むメソッド。
        /// <see cref="BattleScene.Debug.Service.DebugRandomService"/>でメソッド名を利用してリフレクションを行っているため、
        /// NOTE: メソッド名を変更するときは上記クラスも修正すること。
        /// </summary>
        /// <param name="targetIdList">攻撃対象のIDのリスト</param>
        /// <param name="luckRate">成功率</param>
        /// <returns>実際の攻撃対象のIDのリスト</returns>
        public IReadOnlyList<CharacterId> Pick(
            IReadOnlyList<CharacterId> targetIdList,
            float luckRate = 1.0f)
        {
            _orderedItems.First().TryGetCharacterId(out var actorId);
            var actorLuck = _characterPropertyFactory.Create(actorId).Luck;

            var actualTargetList = targetIdList
                .Where(Picks)
                .ToList();

            return actualTargetList;

            bool Picks(CharacterId characterId)
            {
                var targetLuck = _characterPropertyFactory.Create(characterId).Luck;
                var threshold = _battlePropertyFactory.Create().AilmentSuccessThreshold;
                var rate = luckRate * (1.0f + (actorLuck - targetLuck) / threshold);
                return _myRandom.Probability(rate);
            }
        }
    }
}