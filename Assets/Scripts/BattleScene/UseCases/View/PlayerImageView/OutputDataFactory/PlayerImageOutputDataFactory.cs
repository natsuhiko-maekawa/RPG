using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess.ObsoleteIFactory;
using BattleScene.UseCases.View.PlayerImageView.OutputData;

namespace BattleScene.UseCases.View.PlayerImageView.OutputDataFactory
{
    public class PlayerImageOutputDataFactory
    {
        private readonly IAilmentViewInfoFactory _ailmentViewInfoFactory;

        public PlayerImageOutputDataFactory(IAilmentViewInfoFactory ailmentViewInfoFactory)
        {
            _ailmentViewInfoFactory = ailmentViewInfoFactory;
        }

        public PlayerImageOutputData Create(PlayerImageCode playerImageCode)
        {
            return new PlayerImageOutputData(playerImageCode);
        }
    }
}