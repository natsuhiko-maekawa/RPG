using System;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View.DestroyedPartView.OutputData;

namespace BattleScene.UseCases.View.DestroyedPartView.OutputDataFactory
{
    public class DestroyedPartOutputDataFactory
    {
        private readonly IRepository<BodyPartEntity, (CharacterId, BodyPartCode)> _bodyPartRepository;
        private readonly CharacterOutputDataCreatorService _characterOutputDataCreator;
        private readonly ToBodyPartNumberService _toBodyPartNumber;

        public DestroyedPartOutputDataFactory(
            IRepository<BodyPartEntity, (CharacterId, BodyPartCode)> bodyPartRepository,
            CharacterOutputDataCreatorService characterOutputDataCreator,
            ToBodyPartNumberService toBodyPartNumber)
        {
            _bodyPartRepository = bodyPartRepository;
            _characterOutputDataCreator = characterOutputDataCreator;
            _toBodyPartNumber = toBodyPartNumber;
        }

        public DestroyedPartOutputData Create(CharacterId characterId)
        {
            var character = _characterOutputDataCreator.Create(characterId);
            var destroyedPartNumberList = Enum.GetValues(typeof(BodyPartCode))
                .Cast<BodyPartCode>()
                .OrderBy(x => _toBodyPartNumber.BodyPart(x))
                .Select(x => _bodyPartRepository.Select((characterId, x))?.DestroyedPartCount() ?? 0)
                .ToImmutableList();
            return new DestroyedPartOutputData(character, destroyedPartNumberList);
        }
    }
}