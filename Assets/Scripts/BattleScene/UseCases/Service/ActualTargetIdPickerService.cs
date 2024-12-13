using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class ActualTargetIdPickerService : IActualTargetIdPickerService
    {
        private readonly IFactory<BattlePropertyValueObject> _battlePropertyFactory;
        private readonly CharacterPropertyFactoryService _characterPropertyFactory;
        private readonly IMyRandomService _myRandom;

        public ActualTargetIdPickerService(
            IFactory<BattlePropertyValueObject> battlePropertyFactory,
            CharacterPropertyFactoryService characterPropertyFactory,
            IMyRandomService myRandom)
        {
            _battlePropertyFactory = battlePropertyFactory;
            _characterPropertyFactory = characterPropertyFactory;
            _myRandom = myRandom;
        }

        // ReSharper disable once InvalidXmlDocComment
        /// <summary>
        /// 攻撃対象から実際の攻撃対象をランダムに絞り込むメソッド。
        /// <see cref="BattleScene.Debug.Service.DebugRandomService"/>でメソッド名を利用してリフレクションを行っているため、
        /// NOTE: メソッド名を変更するときは上記クラスも修正すること。
        /// </summary>
        /// <param name="targetList">攻撃対象のIDのリスト</param>
        /// <param name="luckRate">成功率</param>
        /// <returns>実際の攻撃対象のIDのリスト</returns>
        public IReadOnlyList<CharacterEntity> Pick(
            CharacterEntity actor,
            IReadOnlyList<CharacterEntity> targetList,
            float luckRate = 1.0f)
        {
            var actorLuck = _characterPropertyFactory.Create(actor.Id).Luck;

            var actualTargetList = targetList
                .Where(Picks)
                .ToArray();

            return actualTargetList;

            bool Picks(CharacterEntity character)
            {
                var targetLuck = _characterPropertyFactory.Create(character.Id).Luck;
                var threshold = _battlePropertyFactory.Create().AilmentSuccessThreshold;
                var rate = luckRate * (1.0f + (actorLuck - targetLuck) / threshold);
                return _myRandom.Probability(rate);
            }
        }
    }
}