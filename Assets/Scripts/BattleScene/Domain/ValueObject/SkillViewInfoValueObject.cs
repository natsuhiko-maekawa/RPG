using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class SkillViewInfoValueObject
    {
        public SkillViewInfoValueObject(
            string skillName,
            PlayerImageCode playerImageCode,
            MessageCode description,
            MessageCode messageCode)
        {
            SkillName = skillName;
            PlayerImageCode = playerImageCode;
            Description = description;
            MessageCode = messageCode;
        }

        public string SkillName { get; }
        public PlayerImageCode PlayerImageCode { get; }
        public MessageCode Description { get; }
        public MessageCode MessageCode { get; }
    }
}