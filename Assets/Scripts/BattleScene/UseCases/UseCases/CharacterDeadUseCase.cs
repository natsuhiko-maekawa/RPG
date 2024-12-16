using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.UseCases.IServices;

namespace BattleScene.UseCases.UseCases
{
    public class CharacterDeadUseCase
    {
        private readonly IAilmentResetService _ailmentReset;
        private readonly IDeadCharacterService _deadCharacter;

        // ReSharper disable once NotAccessedField.Local
        private readonly IDestroyResetService _destroyReset;

        public CharacterDeadUseCase(
            IDeadCharacterService deadCharacter,
            IAilmentResetService ailmentReset,
            IDestroyResetService destroyReset)
        {
            _deadCharacter = deadCharacter;
            _ailmentReset = ailmentReset;
            _destroyReset = destroyReset;
        }

        public bool IsPlayerDeadInThisTurn() => _deadCharacter.IsPlayerDeadInThisTurn();
        public bool IsAllEnemyDead() => _deadCharacter.IsAllEnemyDead();

        public IReadOnlyList<CharacterEntity> GetDeadCharacterInThisTurn()
            => _deadCharacter.GetDeadCharacterInThisTurn();

        public void ConfirmedDead()
        {
            var deadCharacterArray = _deadCharacter.GetDeadCharacterInThisTurn();
            var ailmentCodeArray = Enum.GetValues(typeof(AilmentCode))
                .Cast<AilmentCode>()
                .Where(y => y != AilmentCode.NoAilment)
                .ToArray();
            var resetAilmentLookup = deadCharacterArray
                .SelectMany(_ => ailmentCodeArray, (x, y) => (x, y))
                .ToLookup(x => x.x.Id, x => x.y);
            _ailmentReset.Reset(resetAilmentLookup);

            // TODO: 部位破壊を回復する処理を書くこと。

            _deadCharacter.ConfirmedDead();
        }
    }
}