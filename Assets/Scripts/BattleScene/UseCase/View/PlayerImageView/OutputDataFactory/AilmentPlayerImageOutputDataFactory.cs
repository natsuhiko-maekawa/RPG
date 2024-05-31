using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCase.View.PlayerImageView.OutputData;

namespace BattleScene.UseCase.View.PlayerImageView.OutputDataFactory
{
    public class AilmentPlayerImageOutputDataFactory
    {
        private readonly IAilmentViewInfoFactory _ailmentViewInfoFactory;
        private readonly ICharacterRepository _characterRepository;

        public PlayerImageOutputData Create(AilmentSkillResultValueObject ailmentSkillResult)
        {
            var ailmentCode = ailmentSkillResult.AilmentCode;
            var playerImageCode = _ailmentViewInfoFactory.Create(ailmentCode).PlayerImageCode;
            return new PlayerImageOutputData(playerImageCode);
        }
    }
}