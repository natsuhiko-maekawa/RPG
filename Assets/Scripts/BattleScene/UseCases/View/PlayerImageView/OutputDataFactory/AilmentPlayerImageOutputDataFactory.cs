using BattleScene.Domain.DataAccess.ObsoleteIFactory;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.View.PlayerImageView.OutputData;

namespace BattleScene.UseCases.View.PlayerImageView.OutputDataFactory
{
    public class AilmentPlayerImageOutputDataFactory
    {
        private readonly IAilmentViewInfoFactory _ailmentViewInfoFactory;

        public AilmentPlayerImageOutputDataFactory(
            IAilmentViewInfoFactory ailmentViewInfoFactory)
        {
            _ailmentViewInfoFactory = ailmentViewInfoFactory;
        }

        public PlayerImageOutputData Create(AilmentValueObject ailment)
        {
            var ailmentCode = ailment.AilmentCode;
            var playerImageCode = _ailmentViewInfoFactory.Create(ailmentCode).PlayerImageCode;
            return new PlayerImageOutputData(playerImageCode);
        }
    }
}