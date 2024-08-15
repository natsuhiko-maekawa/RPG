// using System.Linq;
// using BattleScene.Domain.IRepository;
//
// namespace BattleScene.UseCases.Service
// {
//     public class TurnService
//     {
//         private readonly IResultRepository _resultRepository;
//
//         public TurnService(IResultRepository resultRepository)
//         {
//             _resultRepository = resultRepository;
//         }
//
//         public int GetNext()
//         {
//             var lastTurn = _resultRepository.Select()
//                 .Select(x => x.Turn)
//                 .Max(x => x);
//             return lastTurn.Get() + 1;
//         }
//     }
// }