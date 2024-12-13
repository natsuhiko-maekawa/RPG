namespace BattleScene.Presenters.States.Skill
{
    public interface IContext
    {
        public void Select();
        public bool IsContinue { get; }
        public bool IsBreak { get; }
        public StateCode NextStateCode { get; }
    }
}