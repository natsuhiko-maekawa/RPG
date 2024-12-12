using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;

namespace BattleScene.UseCases.IService
{
    public interface IEnemySelectorService
    {
        public void SelectEnemy(CharacterTypeCode[] options, List<CharacterEntity> selected);
    }
}