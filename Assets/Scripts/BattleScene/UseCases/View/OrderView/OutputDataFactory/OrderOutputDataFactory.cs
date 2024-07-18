// using System;
// using System.Collections.Immutable;
// using System.Linq;
// using BattleScene.Domain.Entity;
// using BattleScene.Domain.Id;
// using BattleScene.Domain.IRepository;
// using BattleScene.Domain.ValueObject;
// using BattleScene.UseCases.View.OrderView.OutputData;
//
// namespace BattleScene.UseCases.View.OrderView.OutputDataFactory
// {
//     internal class OrderOutputDataFactory
//     {
//         private readonly ICharacterRepository _characterRepository;
//         private readonly IRepository<OrderedItemEntity, OrderNumber> _orderedItemRepository;
//
//         public OrderOutputDataFactory(
//             ICharacterRepository characterRepository,
//             IRepository<OrderedItemEntity, OrderNumber> orderedItemRepository)
//         {
//             _characterRepository = characterRepository;
//             _orderedItemRepository = orderedItemRepository;
//         }
//
//         public ImmutableList<OrderOutputData> Create()
//         {
//             return _orderedItemRepository.Select()
//                 .Select(x =>
//                 {
//                     return x.ObsoleteOrderedItem switch
//                     {
//                         OrderedCharacterValueObject orderedCharacter
//                             when _characterRepository.Select(orderedCharacter.CharacterId).IsPlayer()
//                             => new OrderOutputData(
//                                 OrderOutputDataType.Player),
//                         OrderedCharacterValueObject orderedCharacter
//                             when !_characterRepository.Select(orderedCharacter.CharacterId).IsPlayer()
//                             => new OrderOutputData(
//                                 OrderOutputDataType.Enemy,
//                                 CharacterTypeId: _characterRepository.Select(orderedCharacter.CharacterId).Property.CharacterTypeId),
//                         OrderedAilmentValueObject orderedAilment => new OrderOutputData(
//                             OrderOutputDataType.Ailment,
//                             AilmentCode: orderedAilment.AilmentCode),
//                         OrderedSlipDamageValueObject orderedSlipDamage => new OrderOutputData(
//                             OrderOutputDataType.Ailment,
//                             SlipDamageCode: orderedSlipDamage.SlipDamageCode),
//                         _ => throw new ArgumentOutOfRangeException()
//                     };
//                 })
//                 .ToImmutableList();
//         }
//     }
// }