using System.Collections.Generic;
using BattleScene.UseCase.View.CharacterVibesView.OutputData;

namespace BattleScene.UseCase.View.CharacterVibesView.OutputBoundary
{
    public interface ICharacterVibesViewPresenter
    {
        public void Start(CharacterVibesOutputData characterVibesOutputData);
        public void Start(IList<CharacterVibesOutputData> characterVibesOutputDataList);
    }
}