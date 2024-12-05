using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BattleScene.DataAccess.Dto;
using BattleScene.DataAccess.Factory;
using BattleScene.DataAccess.Resource;
using BattleScene.DataAccess.ScriptableObjects;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.Service.Order;
using NUnit.Framework;
using Tests.BattleScene.DataAccess.Repository;
using Tests.BattleScene.DataAccess.Resource;
using UnityEditor;
using UnityEngine;

namespace Tests.BattleScene.UseCases.Service
{
    [TestFixture]
    public class OrderServiceTest
    {
        private IFactory<BattlePropertyValueObject> _stubBattlePropertyFactory;

        private readonly ICollection<AilmentEntity, (CharacterId, AilmentCode)> _mockAilmentCollection
            = new MockCollection<AilmentEntity, (CharacterId, AilmentCode)>();

        private readonly ICollection<BodyPartEntity, (CharacterId, BodyPartCode)> _mockBodyPartCollection
            = new MockCollection<BodyPartEntity, (CharacterId, BodyPartCode)>();

        private readonly ICollection<BuffEntity, (CharacterId, BuffCode)> _mockBuffCollection
            = new MockCollection<BuffEntity, (CharacterId, BuffCode)>();

        private readonly ICollection<CharacterEntity, CharacterId> _mockCharacterCollection
            = new MockCollection<CharacterEntity, CharacterId>();

        private readonly ICollection<EnhanceEntity, (CharacterId, EnhanceCode)> _mockEnhanceCollection
            = new MockCollection<EnhanceEntity, (CharacterId, EnhanceCode)>();

        private readonly ICollection<OrderedItemEntity, OrderedItemId> _mockOrderedItemCollection
            = new MockCollection<OrderedItemEntity, OrderedItemId>();

        private readonly ICollection<SlipEntity, SlipCode> _mockSlipCollection
            = new MockCollection<SlipEntity, SlipCode>();

        private OrderService _orderService = null!;
        private ISpeedService _stubSpeedService = null!;
        private ICharacterCreatorService _stubCharacterCreatorService = null!;

        [SetUp]
        public void SetUp()
        {
            _mockAilmentCollection.Remove();
            _mockBuffCollection.Remove();
            _mockCharacterCollection.Remove();
            _mockOrderedItemCollection.Remove();
            _mockSlipCollection.Remove();
            _stubBattlePropertyFactory = GetStubBattlePropertyFactory();
            _stubSpeedService = GetStubSpeedService();
            _stubCharacterCreatorService = GetStubCharacterCreatorService();
            _orderService = GetOrderService();
            _orderService.Initialize();
        }

        // Creation method pattern.

        #region ForSetUp

        private IFactory<BattlePropertyValueObject> GetStubBattlePropertyFactory()
        {
            var battlePropertyResource = AssetDatabase.LoadAssetAtPath<BattlePropertyResource>(
                "Assets/Prefabs/BattleScene/Resource/BattleSceneProperty.prefab");
            var stubBattlePropertyFactory = new BattlePropertyFactory(
                battlePropertyResource: battlePropertyResource);
            return stubBattlePropertyFactory;
        }

        private ISpeedService GetStubSpeedService()
        {
            var characterPropertyScriptableObject
                = AssetDatabase.LoadAssetAtPath<BaseScriptableObject<CharacterPropertyDto, CharacterTypeCode>>(
                    "Assets/ScriptableObject/CharacterPropertyScriptableObject.asset");
            var stubCharacterPropertyResource
                = new StubBaseScriptableObjectResource<CharacterPropertyDto, CharacterTypeCode>(
                    itemList: characterPropertyScriptableObject.ItemList);
            var stubCharacterPropertyFactory = new CharacterPropertyFactory(
                propertyResource: stubCharacterPropertyResource);
            var stubCharacterPropertyFactoryService = new CharacterPropertyFactoryService(
                characterPropertyFactory: stubCharacterPropertyFactory,
                characterCollection: _mockCharacterCollection);
            var stubSpeedService = new SpeedService(
                buffCollection: _mockBuffCollection,
                characterPropertyFactory: stubCharacterPropertyFactoryService);
            return stubSpeedService;
        }

        private ICharacterCreatorService GetStubCharacterCreatorService()
        {
            var stubAilmentPropertyResource = AssetDatabase.LoadAssetAtPath<AilmentPropertyResource>(
                "Assets/Prefabs/BattleScene/Resource/ScriptableObjects.prefab");
            var stubAilmentPropertyFactory = new AilmentPropertyFactory(stubAilmentPropertyResource);
            var stubBodyPartPropertyResource = AssetDatabase.LoadAssetAtPath<BodyPartPropertyResource>(
                "Assets/Prefabs/BattleScene/Resource/ScriptableObjects.prefab");
            var stubBodyPartPropertyFactory = new BodyPartPropertyFactory(stubBodyPartPropertyResource);
            var stubCharacterCreatorService = new CharacterCreatorService(
                ailmentCollection: _mockAilmentCollection,
                ailmentPropertyFactory: stubAilmentPropertyFactory,
                battlePropertyFactory: _stubBattlePropertyFactory,
                bodyPartPropertyFactory: stubBodyPartPropertyFactory,
                bodyPartCollection: _mockBodyPartCollection,
                buffCollection: _mockBuffCollection,
                enhanceCollection: _mockEnhanceCollection,
                slipCollection: _mockSlipCollection);
            return stubCharacterCreatorService;
        }

        private OrderService GetOrderService()
        {
            var orderService = new OrderService(
                battlePropertyFactory: _stubBattlePropertyFactory,
                ailmentCollection: _mockAilmentCollection,
                characterCollection: _mockCharacterCollection,
                orderedItemCollection: _mockOrderedItemCollection,
                slipDamageCollection: _mockSlipCollection,
                speed: _stubSpeedService);
            return orderService;
        }

        #endregion

        #region ForTests

        private OrderedItemEntity[] GetExpectedValue(object[] items)
        {
            return items
                .Select(GetOrderedItem)
                .ToArray();
        }

        private static OrderedItemEntity GetOrderedItem(object expected, int i)
        {
            switch (expected)
            {
                case CharacterId characterId:
                {
                    var orderedItem = new OrderedItemEntity(new OrderedItemId(), i);
                    orderedItem.SetOrderedItem(new OrderedItem(characterId));
                    return orderedItem;
                }
                case AilmentCode ailmentCode:
                {
                    var orderedItem = new OrderedItemEntity(new OrderedItemId(), i);
                    orderedItem.SetOrderedItem(new OrderedItem(ailmentCode));
                    return orderedItem;
                }
                case SlipCode slipCode:
                {
                    var orderedItem = new OrderedItemEntity(new OrderedItemId(), i);
                    orderedItem.SetOrderedItem(new OrderedItem(slipCode));
                    return orderedItem;
                }
                default:
                    throw new InvalidCastException();
            }
        }

        private (CharacterId, string) AddPlayer()
        {
            var playerId = new CharacterId();
            var player = new CharacterEntity(
                id: playerId,
                characterTypeCode: CharacterTypeCode.Player,
                currentHitPoint: 179,
                currentTechnicalPoint: 50);
            _mockCharacterCollection.Add(player);
            _stubCharacterCreatorService.Create(playerId, isPlayer: true);
            return (playerId, "Player");
        }

        private (CharacterId, string) AddBee()
        {
            var beeId = new CharacterId();
            var bee = new CharacterEntity(
                id: beeId,
                characterTypeCode: CharacterTypeCode.Bee,
                currentHitPoint: 39,
                position: 0);
            _mockCharacterCollection.Add(bee);
            _stubCharacterCreatorService.Create(beeId);
            return (beeId, "Bee");
        }

        private (CharacterId, string) AddBee2()
        {
            var beeId = new CharacterId();
            var bee = new CharacterEntity(
                id: beeId,
                characterTypeCode: CharacterTypeCode.Bee,
                currentHitPoint: 39,
                position: 0);
            _mockCharacterCollection.Add(bee);
            _stubCharacterCreatorService.Create(beeId);
            return (beeId, "Bee");
        }

        private (CharacterId, string) AddSlime()
        {
            var slimeId = new CharacterId();
            var slime = new CharacterEntity(
                id: slimeId,
                characterTypeCode: CharacterTypeCode.Slime,
                currentHitPoint: 10,
                position: 1);
            _mockCharacterCollection.Add(slime);
            _stubCharacterCreatorService.Create(slimeId);
            return (slimeId, "Slime");
        }

        private (CharacterId, string) AddDragon()
        {
            var dragonId = new CharacterId();
            var dragon = new CharacterEntity(
                id: dragonId,
                characterTypeCode: CharacterTypeCode.Dragon,
                currentHitPoint: 56,
                position: 2);
            _mockCharacterCollection.Add(dragon);
            _stubCharacterCreatorService.Create(dragonId);
            return (dragonId, "Dragon");
        }

        private (CharacterId, string) AddShuten()
        {
            var shutenId = new CharacterId();
            var shuten = new CharacterEntity(
                id: shutenId,
                characterTypeCode: CharacterTypeCode.Shuten,
                currentHitPoint: 56,
                position: 3);
            _mockCharacterCollection.Add(shuten);
            _stubCharacterCreatorService.Create(shutenId);
            return (shutenId, "Shuten");
        }

        #endregion

        [Test]
        public void プレイヤーと敵2体の行動順()
        {
            var (playerId, playerName) = AddPlayer();
            var (beeId, beeName) = AddBee();
            var (slimeId, slimeName) = AddSlime();

            _orderService.Update();
            var actual = _mockOrderedItemCollection.Get();

            var items = new object[]
            {
                playerId, beeId, slimeId, playerId, beeId, playerId, slimeId, beeId, playerId,
                playerId, beeId, playerId, slimeId, beeId
            };
            var expected = GetExpectedValue(items);

            Assert.That(actual, Is.EqualTo(expected).Using(new OrderedItemEqualityComparator()));
        }

        [Test]
        public void プレイヤーと同種の敵2体の行動順()
        {
            var (playerId, playerName) = AddPlayer();
            var (beeId, beeName) = AddBee();
            var (bee2Id, bee2Name) = AddBee2();

            _orderService.Update();
            var actual = _mockOrderedItemCollection.Get();

            var items = new object[]
            {
                playerId, beeId, bee2Id, playerId, beeId, bee2Id, playerId, beeId, bee2Id,
                playerId, playerId, beeId, bee2Id, playerId
            };
            var items2 = new object[]
            {
                playerId, bee2Id, beeId, playerId, bee2Id, beeId, playerId, bee2Id, beeId,
                playerId, playerId, bee2Id, beeId, playerId
            };
            var expected = GetExpectedValue(beeId.CompareTo(bee2Id) < 0 ? items : items2);

            Assert.That(actual, Is.EqualTo(expected).Using(new OrderedItemEqualityComparator()));
        }

        [Test]
        public void プレイヤーと敵3体の行動順()
        {
            var (playerId, playerName) = AddPlayer();
            var (beeId, beeName) = AddBee();
            var (slimeId, slimeName) = AddSlime();
            var (dragonId, dragonName) = AddDragon();

            _orderService.Update();
            var actual = _mockOrderedItemCollection.Get();

            var items = new object[]
            {
                playerId, beeId, slimeId, dragonId, playerId, beeId, playerId, slimeId, beeId,
                playerId, dragonId, playerId, beeId, playerId
            };
            var expected = GetExpectedValue(items);

            Assert.That(actual, Is.EqualTo(expected).Using(new OrderedItemEqualityComparator()));
        }

        [Test]
        public void プレイヤーと敵4体の行動順()
        {
            var (playerId, playerName) = AddPlayer();
            var (beeId, beeName) = AddBee();
            var (slimeId, slimeName) = AddSlime();
            var (dragonId, dragonName) = AddDragon();
            var (shutenId, shutenName) = AddShuten();

            _orderService.Update();
            var actual = _mockOrderedItemCollection.Get();

            var items = new object[]
            {
                playerId, beeId, slimeId, dragonId, shutenId, playerId, beeId, playerId, slimeId,
                beeId, playerId, dragonId, shutenId, playerId
            };
            var expected = GetExpectedValue(items);

            Assert.That(actual, Is.EqualTo(expected).Using(new OrderedItemEqualityComparator()));
        }

        [Test]
        public void プレイヤーと敵4体とスリップダメージの行動順()
        {
            var (playerId, playerName) = AddPlayer();
            var (beeId, beeName) = AddBee();
            var (slimeId, slimeName) = AddSlime();
            var (dragonId, dragonName) = AddDragon();
            var (shutenId, shutenName) = AddShuten();

            var poisoning = _mockSlipCollection.Get(SlipCode.Poisoning);
            poisoning.Effects = true;
            poisoning.AdvanceTurn();

            _orderService.Update();
            var actual = _mockOrderedItemCollection.Get();

            var items = new object[]
            {
                playerId, beeId, slimeId, dragonId, SlipCode.Poisoning, shutenId, playerId,
                beeId, playerId, SlipCode.Poisoning, slimeId, beeId, playerId, dragonId
            };
            var expected = GetExpectedValue(items);

            Assert.That(actual, Is.EqualTo(expected).Using(new OrderedItemEqualityComparator()));
        }

        private static void MockOrderedItemRepositoryLogger(
            ICollection<OrderedItemEntity, OrderedItemId> orderedItemRepository,
            IReadOnlyList<(CharacterId, string)> characterIdNamePairList)
        {
            var log = orderedItemRepository.ToString();
            foreach (var (characterId, characterName) in characterIdNamePairList)
            {
                log = log.Replace(characterId.ToString(), characterName);
            }

            Debug.Log(log);
        }
    }
}