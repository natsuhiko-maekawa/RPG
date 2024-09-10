using BattleScene.Domain.Code;
using BattleScene.UseCases.View.MessageView.OutputData;

namespace BattleScene.UseCases.Output
{
    public class ActionDescriptionMessageOutput
    {
        public MessageOutputData Out(ActionCode actionCode)
        {
            var messageCode = actionCode switch
            {
                ActionCode.Attack => MessageCode.AttackDescription,
                ActionCode.Skill => MessageCode.SkillDescription,
                ActionCode.Defence => MessageCode.DefenceDescription,
                ActionCode.FatalitySkill => MessageCode.FatalitySkillDescription,
                _ => MessageCode.NoMessage
            };

            var outputData = new MessageOutputData(
                MessageCode: messageCode,
                NoWait: true);
            return outputData;
        }
    }
}