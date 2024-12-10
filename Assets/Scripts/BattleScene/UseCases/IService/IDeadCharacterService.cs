namespace BattleScene.UseCases.IService
{
    public interface IDeadCharacterService
    {
        public bool DeadInThisTurn();
        public void ConfirmedDead();
    }
}