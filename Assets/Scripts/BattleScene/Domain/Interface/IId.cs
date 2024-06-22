namespace BattleScene.Domain.Interface
{
    public interface IId
    {
        public bool Equals(object obj);
        public int GetHashCode();
    }
}