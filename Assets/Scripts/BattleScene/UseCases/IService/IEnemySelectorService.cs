using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;

namespace BattleScene.UseCases.IService
{
    public interface IEnemySelectorService
    {
        public void SelectEnemy(CharacterTypeCode[] options, List<CharacterEntity> selected);
    }
}