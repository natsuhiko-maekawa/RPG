using BattleScene.Domain.Code;
using BattleScene.UseCases.View.PlayerImageView.OutputData;

namespace BattleScene.UseCases.View.PlayerImageView.OutputDataFactory
{
    public class PlayerImageOutputDataFactory
    {
        public PlayerImageOutputData Create(PlayerImageCode playerImageCode)
        {
            return new PlayerImageOutputData(playerImageCode);
        }
    }
}