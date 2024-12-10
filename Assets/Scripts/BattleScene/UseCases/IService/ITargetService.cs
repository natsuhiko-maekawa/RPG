using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;

namespace BattleScene.UseCases.IService
{
    public interface ITargetService
    {
        public IReadOnlyList<CharacterEntity> Get(CharacterEntity actor, Range range, bool isAutoTarget=false);
        public void GetOption(CharacterEntity actor, Range range, List<CharacterEntity> optionTargetList);
    }
}