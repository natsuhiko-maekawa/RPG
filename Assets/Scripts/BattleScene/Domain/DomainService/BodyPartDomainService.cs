using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.DomainService
{
    public class BodyPartDomainService
    {
        private readonly ICollection<BodyPartEntity, (CharacterId, BodyPartCode)> _bodyPartCollection;

        public BodyPartDomainService(
            ICollection<BodyPartEntity, (CharacterId, BodyPartCode)> bodyPartCollection)
        {
            _bodyPartCollection = bodyPartCollection;
        }

        public int Count(CharacterId characterId, BodyPartCode bodyPartCode)
        {
            var bodyPartEntity = _bodyPartCollection.Get()
                .FirstOrDefault(x => Equals(x.CharacterId, characterId) && x.BodyPartCode == bodyPartCode);
            return bodyPartEntity?.DestroyedCount ?? 0;
        }

        public bool IsAvailable(CharacterId characterId, BodyPartCode bodyPartCode)
        {
            var bodyPartEntity = _bodyPartCollection.Get()
                .FirstOrDefault(x => Equals(x.CharacterId, characterId) && x.BodyPartCode == bodyPartCode);
            return bodyPartEntity?.IsAvailable() ?? true;
        }

        public bool IsAvailable(CharacterId characterId, IList<BodyPartCode> bodyPartCodeList)
        {
            if (bodyPartCodeList.Count == 0) return true;
            return bodyPartCodeList
                .Select(x => IsAvailable(characterId, x))
                .Aggregate((x, y) => x && y);
        }
    }
}