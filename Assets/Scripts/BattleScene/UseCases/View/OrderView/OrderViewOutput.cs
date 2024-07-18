// using BattleScene.UseCases.Output.Interface;
// using BattleScene.UseCases.View.OrderView.OutputBoundary;
// using BattleScene.UseCases.View.OrderView.OutputDataFactory;
//
// namespace BattleScene.UseCases.View.OrderView
// {
//     internal class OrderViewOutput
//     {
//         private readonly OrderOutputDataFactory _orderOutputDataFactory;
//         private readonly IOrderViewPresenter _orderView;
//
//         public OrderViewOutput(
//             OrderOutputDataFactory orderOutputDataFactory, 
//             IOrderViewPresenter orderView)
//         {
//             _orderOutputDataFactory = orderOutputDataFactory;
//             _orderView = orderView;
//         }
//
//         public void Out()
//         {
//             _orderView.Start(_orderOutputDataFactory.Create());
//         }
//     }
// }