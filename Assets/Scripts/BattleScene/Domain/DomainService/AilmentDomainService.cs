using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

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
        ///     すべての状態異常のターンを1ターン進める。
        /// </summary>
        public void AdvanceTurn()
        {
            foreach (var ailment in _ailmentRepository.Get())
            {
                ailment.AdvanceTurn();
            }
        }

        /// <summary>
        ///     行動不能となる状態異常のコードを優先度が高い順にソートして返す。
        /// </summary>
        /// <param name="character">キャラクターID</param>
        /// <returns>状態異常のコードのリスト</returns>
        public IReadOnlyList<AilmentCode> GetCantActionAilmentCodeList(CharacterEntity character)
        {
            // QUESTION: どちらのLINQのほうが良いかわからない。
            // var ailmentCodeArray = _ailmentRepository.Get()
            //     .Where(x => x.CharacterId == character.Id)
            //     .Where(x => x.Effects)
            //     .Where(x => _ailmentPropertyFactory.Create(x.AilmentCode).Priority.HasValue)
            //     .OrderBy(x => _ailmentPropertyFactory.Create(x.AilmentCode).Priority)
            //     .Select(x => x.AilmentCode)
            //     .ToArray();

            var ailmentCodeArray = Enum.GetValues(typeof(AilmentCode))
                .Cast<AilmentCode>()
                .Where(ailmentCode => ailmentCode != AilmentCode.NoAilment)
                .ToArray();
            var ailments = ailmentCodeArray
                .Select(ailmentCode => _ailmentRepository.Get((character.Id, ailmentCode)))
                .ToArray();
            var ailmentProperties = ailmentCodeArray
                .Select(ailmentCode => _ailmentPropertyFactory.Create(ailmentCode))
                .ToArray();
            var ailmentCodeList = ailments
                .Where(ailment => ailment.Effects)
                .Join(inner: ailmentProperties,
                    outerKeySelector: outer => outer.AilmentCode,
                    innerKeySelector: inner => inner.AilmentCode,
                    resultSelector: (_, inner) => inner)
                .Where(ailmentProperty => ailmentProperty.Priority.HasValue)
                .OrderBy(ailmentProperty => ailmentProperty.Priority)
                .Select(ailmentProperty => ailmentProperty.AilmentCode)
                .ToArray();
            return ailmentCodeList;
        }
    }
}