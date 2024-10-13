using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using JetBrains.Annotations;

namespace BattleScene.Domain.DomainService
{
    public class AilmentDomainService
    {
        private readonly IFactory<AilmentPropertyValueObject, AilmentCode> _ailmentPropertyFactory;
        private readonly ICollection<AilmentEntity, (CharacterId, AilmentCode)> _ailmentCollection;

        public AilmentDomainService(
            IFactory<AilmentPropertyValueObject, AilmentCode> ailmentPropertyFactory,
            ICollection<AilmentEntity, (CharacterId, AilmentCode)> ailmentCollection)
        {
            _ailmentPropertyFactory = ailmentPropertyFactory;
            _ailmentCollection = ailmentCollection;
        }

        /// <summary>
        ///     状態異常を効果時間が短い順にソートして返す。
        /// </summary>
        /// <returns>状態異常エンティティのリスト</returns>
        public IReadOnlyList<AilmentEntity> GetOrdered(CharacterId characterId)
        {
            return _ailmentCollection.Get()
                .Where(x => Equals(x.CharacterId, characterId))
                .Where(x => x.IsSelfRecovery)
                .OrderBy(x => x.Turn)
                .ThenBy(x => x)
                .ToList();
        }

        /// <summary>
        ///     すべての状態異常のターンを1ターン進める。
        /// </summary>
        public void AdvanceTurn()
        {
            foreach (var ailment in _ailmentCollection.Get()) ailment.AdvanceTurn();
        }

        /// <summary>
        ///     有効な状態異常のうち最も優先度の高いものを返す。
        ///     該当する状態異常がない場合はnullを返す。
        /// </summary>
        /// <param name="characterId">キャラクターID</param>
        /// <returns>状態異常エンティティ、もしくはnull</returns>
        [CanBeNull]
        public AilmentEntity GetHighestPriority(CharacterId characterId)
        {
            var ailment = _ailmentCollection.Get()
                .Where(x => Equals(x.CharacterId, characterId))
                .Where(x => x.Effects)
                .Where(x => _ailmentPropertyFactory.Create(x.AilmentCode).Priority.HasValue)
                .OrderBy(x => _ailmentPropertyFactory.Create(x.AilmentCode).Priority)
                .FirstOrDefault();
            return ailment;
        }
    }
}