using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.UseCase.View.PlayerImageView.OutputData;

namespace BattleScene.UseCase.View.PlayerImageView.OutputDataFactory
{
    public class PlayerImageOutputDataFactory
    {
        private readonly IAilmentViewInfoFactory _ailmentViewInfoFactory;

        public PlayerImageOutputData Create(PlayerImageCode playerImageCode)
        {
            return new PlayerImageOutputData(playerImageCode);
        }
    }
}