using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class CharacterCreatorService : ICharacterCreatorService
    {
        private readonly ICollection<BuffEntity, (CharacterId, BuffCode)> _buffCollection;

        public CharacterCreatorService(
            ICollection<BuffEntity, (CharacterId, BuffCode)> buffCollection)
        {
            _buffCollection = buffCollection;
        }

        public void Create(CharacterId characterId)
        {
            var buffCodes = Enum.GetValues(typeof(BuffCode))
                .Cast<BuffCode>()
                .Where(x => x != BuffCode.NoBuff);
            foreach (var buffCode in buffCodes)
            {
                var buff = new BuffEntity(
                    characterId: characterId,
                    buffCode: buffCode);
                _buffCollection.Add(buff);
            }
        }

        public void Create(IEnumerable<CharacterId> characterIdList)
        {
            foreach (var characterId in characterIdList) Create(characterId);
        }
    }
}