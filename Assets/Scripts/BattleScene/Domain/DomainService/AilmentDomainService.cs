using System.Collections.Immutable;
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
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;

        public AilmentDomainService(
            IFactory<AilmentPropertyValueObject, AilmentCode> ailmentPropertyFactory,
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository)
        {
            _ailmentPropertyFactory = ailmentPropertyFactory;
            _ailmentRepository = ailmentRepository;
        }

        /// <summary>
        ///     状態異常を効果時間が短い順にソートして返す。
        /// </summary>
        /// <returns>状態異常エンティティのリスト</returns>
        public ImmutableList<AilmentEntity> GetOrdered(CharacterId characterId)
        {
            return _ailmentRepository.Select()
                .Where(x => Equals(x.CharacterId, characterId))
                .Where(x => x.IsSelfRecovery)
                .OrderBy(x => x.Turn)
                .ThenBy(x => x)
                .ToImmutableList();
        }

        /// <summary>
        ///     すべての状態異常のターンを1ターン進め、無効になった状態異常を削除する。
        /// </summary>
        public void AdvanceTurn()
        {
            var ailments = _ailmentRepository.Select()
                .Select(x =>
                {
                    x.AdvanceTurn();
                    return x;
                })
            .ToImmutableList();
            _ailmentRepository.Update(ailments);

            var recoverAilmentsList = _ailmentRepository.Select()
                .Where(x => x.TurnIsEnd)
                .Select(x => x.Id)
                .ToImmutableList();
            _ailmentRepository.Delete(recoverAilmentsList);
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
            var ailment = _ailmentRepository.Select()
                .Where(x => Equals(x.CharacterId, characterId))
                .Where(x => x.Effects)
                .Where(x => _ailmentPropertyFactory.Create(x.AilmentCode).Priority.HasValue)
                .OrderBy(x => _ailmentPropertyFactory.Create(x.AilmentCode).Priority)
                .FirstOrDefault();
            return ailment;
        }
    }
}