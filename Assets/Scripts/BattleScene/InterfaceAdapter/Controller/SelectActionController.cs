// using BattleScene.Domain.Code;
// using BattleScene.UseCases;
//
// namespace BattleScene.InterfaceAdapter.Controller
// {
//     public class SelectActionController
//     {
//         private readonly StateMachine _stateMachine;
//
//         public void Start()
//         {
//             _stateMachine.Start();
//         }
//
//         public void Select()
//         {
//             _stateMachine.Select();
//         }
//         
//         public void SelectAction(int id)
//         {
//             var actionCode = (ActionCode)id;
//             _stateMachine.Select(actionCode);
//         }
//     }
// }