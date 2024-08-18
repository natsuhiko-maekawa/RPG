using BattleScene.Domain.Entity;
using BattleScene.UseCases.View.SelectSkillView.OutputData;

namespace BattleScene.UseCases.Event
{
    public interface IState
    {
        public record SkillState(SkillEntity SkillEntity) : IState;

        public class OrderState : IState
        {
            private OrderState(){}

            public static OrderState Create()
            {
                return new OrderState();
            }
        }
    }
    
}