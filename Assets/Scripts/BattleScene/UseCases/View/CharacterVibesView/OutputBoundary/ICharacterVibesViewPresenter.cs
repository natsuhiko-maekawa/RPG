using System.Collections.Generic;
using BattleScene.UseCases.View.CharacterVibesView.OutputData;

namespace BattleScene.UseCases.View.CharacterVibesView.OutputBoundary
{
    public interface ICharacterVibesViewPresenter
    {
        public void Start(CharacterVibesOutputData characterVibesOutputData);
        public void Start(IList<CharacterVibesOutputData> characterVibesOutputDataList);
    }
}