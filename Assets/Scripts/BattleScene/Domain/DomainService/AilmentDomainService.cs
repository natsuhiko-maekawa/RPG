using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.DomainService
{
    public class AilmentDomainService
    {
        private readonly IAilmentRepository _ailmentRepository;

        public AilmentDomainService(
            IAilmentRepository ailmentRepository)
        {
            _ailmentRepository = ailmentRepository;
        }

        /// <summary>
        ///     状態異常を効果時間が短い順にソートして返す。
        /// </summary>
        /// <returns>状態異常エンティティのリスト</returns>
        public ImmutableList<AilmentEntity> GetOrdered(CharacterId characterId)
        {
            return _ailmentRepository.Select(characterId)
                .Where(x => x.GetTurn() != null)
                .OrderBy(x => x.GetTurn())
                .ThenBy(x => x)
                .ToImmutableList();
        }

        /// <summary>
        ///     すべての状態異常のターンを1ターン進め、無効になった状態異常を削除する。
        /// </summary>
        public void AdvanceAllTurn(CharacterId characterId)
        {
            foreach (var ailmentEntity in _ailmentRepository.Select(characterId))
                ailmentEntity.AdvanceTurn();

            var recoverAilmentsList = _ailmentRepository.Select(characterId)
                .Where(x => x.TurnIsEnd())
                .ToImmutableList();

            _ailmentRepository.Delete(recoverAilmentsList);
        }

        /// <summary>
        ///     有効な状態異常のうち最も優先度の高いものを返す。
        /// </summary>
        /// <returns>状態異常エンティティ</returns>
        public AilmentEntity GetHighPriority(CharacterId characterId)
        {
            return _ailmentRepository.Select(characterId)
                .OrderByDescending(x => x.Priority)
                .FirstOrDefault();
        }
    }
}