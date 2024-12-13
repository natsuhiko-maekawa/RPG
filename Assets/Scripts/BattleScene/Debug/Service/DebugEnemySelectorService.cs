using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using Common;
using UnityEngine;
using VContainer;
using static BattleScene.Domain.Code.CharacterTypeCode;

namespace BattleScene.Debug.Service
{
    public class DebugEnemySelectorService : MonoBehaviour, IEnemySelectorService
    {
        public string[] characterTypeCodeArray = Array.Empty<string>();
        private CharacterTypeCode[] _characterTypeCodeArray;
        private IFactory<CharacterPropertyValueObject, CharacterTypeCode> _characterPropertyFactory;

        [Inject]
        public void Construct(
            IFactory<CharacterPropertyValueObject, CharacterTypeCode> characterPropertyFactory)
        {
            _characterPropertyFactory = characterPropertyFactory;
        }

        private void Reset()
        {
            characterTypeCodeArray = new[] { Bee.ToString(), Slime.ToString(), Dragon.ToString(), Shuten.ToString() };
        }

        private void OnValidate()
        {
            if (characterTypeCodeArray.Length == 0)
            {
                characterTypeCodeArray = new[] { Bee.ToString() };
                throw new SerializationException("少なくとも一体の敵を選択してください。");
            }

            if (characterTypeCodeArray.Length > Constant.MaxEnemyCount)
            {
                characterTypeCodeArray = characterTypeCodeArray[..Constant.MaxEnemyCount];
                throw new SerializationException("敵を4体以上選択することはできません。");
            }

            _characterTypeCodeArray = characterTypeCodeArray
                .Select(Enum.Parse<CharacterTypeCode>)
                .ToArray();

            if (_characterTypeCodeArray
                .Any(x => x is None or Player))
            {
                _characterTypeCodeArray = _characterTypeCodeArray
                    .Where(x => x is not None and not Player)
                    .ToArray();
                throw new SerializationException("プレイヤーやデフォルト値を選択することはできません。");
            }
        }

        public void SelectEnemy(CharacterTypeCode[] options, List<CharacterEntity> selected)
        {
            selected.Clear();
            var characterArray = _characterTypeCodeArray
                .Select(x => _characterPropertyFactory.Create(x))
                .Select((characterProperty, index) =>
                {
                    var characterId = new CharacterId();
                    return new CharacterEntity(
                        id: characterId,
                        characterTypeCode: characterProperty.CharacterTypeCode,
                        currentHitPoint: characterProperty.HitPoint,
                        position: index);
                })
                .ToArray();
            selected.AddRange(characterArray);
        }
    }
}