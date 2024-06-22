// using System.Collections.Generic;
// using System.Collections.Immutable;
// using System.Linq;
// using BattleScene.Domain.Code;
// using BattleScene.Domain.Entity;
// using BattleScene.Domain.IRepository;
//
// namespace BattleScene.InterfaceAdapter.DataAccess.Repository
// {
//     public class SlipDamageRepository : ISlipDamageRepository
//     {
//         private readonly HashSet<SlipDamageEntity> _slipDamageSet = new();
//         
//         public ImmutableList<SlipDamageEntity> Select()
//         {
//             return _slipDamageSet.ToImmutableList();
//         }
//
//         public SlipDamageEntity Select(SlipDamageCode slipDamageCode)
//         {
//             return _slipDamageSet
//                 .First(x => x.SlipDamageCode == slipDamageCode);
//         }
//     }
// }