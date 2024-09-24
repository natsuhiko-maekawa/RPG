namespace BattleScene.InterfaceAdapter.State.PrimeSkill
{
    public interface IContext
    {
        public void Select();
        public bool IsContinue { get; }
        public bool IsBreak { get; }
    }
}