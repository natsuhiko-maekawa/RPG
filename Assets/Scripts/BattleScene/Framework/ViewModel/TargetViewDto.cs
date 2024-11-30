using System.Collections.Generic;

namespace BattleScene.Framework.ViewModel
{
    public struct TargetViewDto
    {
        public IReadOnlyList<CharacterStruct> CharacterDtoList { get; }

        public TargetViewDto(IReadOnlyList<CharacterStruct> characterStructList)
        {
            CharacterDtoList = characterStructList;
        }
    }
}