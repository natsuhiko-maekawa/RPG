// using BattleScene.UseCases.StateMachine;
// using BattleScene.UseCases.UseCase;
// using BattleScene.UseCases.View.AilmentView;
//
// namespace BattleScene.UseCases.Event
// {
//     internal class InitializationEvent : BaseEvent
//     {
//         private readonly Initialization _initialization;
//         private readonly AilmentViewOutput _ailmentView;
//
//         public InitializationEvent(
//             Initialization initialization, 
//             AilmentViewOutput ailmentView)
//         {
//             _initialization = initialization;
//             _ailmentView = ailmentView;
//         }
//
//         public override StateCode GetStateCode()
//         {
//             return StateCode.InitializeEnemy;
//         }
//
//         public override void UseCase()
//         {
//             _initialization.Execute();
//         }
//
//         public override void Output()
//         {
//             _ailmentView.Out();
//         }
//     }
// }