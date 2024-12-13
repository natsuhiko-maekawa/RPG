using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;

namespace BattleScene.UseCases.IServices
{
    public interface ITargetService
    {
        public IReadOnlyList<CharacterEntity> Get(CharacterEntity actor, Range range, bool isAutoTarget=false);
        public void GetOption(CharacterEntity actor, Range range, List<CharacterEntity> optionTargetList);
    }
}