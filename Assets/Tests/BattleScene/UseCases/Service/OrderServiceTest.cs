using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.DataAccess.Dto;
using BattleScene.DataAccess.Factory;
using BattleScene.DataAccess.Resource;
using BattleScene.DataAccess.ScriptableObjects;
using BattleScene.Domain.Codes;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.Domain.ValueObjects;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.Service.Order;
using NUnit.Framework;
using Tests.BattleScene.DataAccess.Repository;
using Tests.BattleScene.DataAccess.Resource;
using UnityEditor;
using UnityEngine;
using AilmentEntity = BattleScene.Domain.Entities.AilmentEntity;
using BodyPartEntity = BattleScene.Domain.Entities.BodyPartEntity;
using BuffEntity = BattleScene.Domain.Entities.BuffEntity;
using CharacterEntity = BattleScene.Domain.Entities.CharacterEntity;
using SlipEntity = BattleScene.Domain.Entities.SlipEntity;

// ReSharper disable UnusedVariable

namespace Tests.BattleScene.UseCases.Service
{
    [TestFixture]
    public class OrderServiceTest
    {
        private IFactory<BattlePropertyValueObject> _stubBattlePropertyFactory;

        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _mockAilmentRepository
            = new MockRepository<AilmentEntity, (CharacterId, AilmentCode)>();

        private readonly IRepository<BodyPartEntity, (CharacterId, BodyPartCode)> _mockBodyPartRepository
            = new MockRepository<BodyPartEntity, (CharacterId, BodyPartCode)>();

        private readonly IRepository<BuffEntity, (CharacterId, BuffCode)> _mockBuffRepository
            = new MockRepository<BuffEntity, (CharacterId, BuffCode)>();

        private readonly IRepository<CharacterEntity, CharacterId> _mockCharacterRepository
            = new MockRepository<CharacterEntity, CharacterId>();

        private readonly IRepository<EnhanceEntity, (CharacterId, EnhanceCode)> _mockEnhanceRepository
            = new MockRepository<EnhanceEntity, (CharacterId, EnhanceCode)>();

        private readonly IRepository<OrderedItemEntity, OrderedItemId> _mockOrderedItemRepository
            = new MockRepository<OrderedItemEntity, OrderedItemId>();

        private readonly IRepository<SlipEntity, SlipCode> _mockSlipRepository
            = new MockRepository<SlipEntity, SlipCode>();

        private OrderService _orderService = null!;
        private ISpeedService _stubSpeedService = null!;
        private ICharacterCreatorService _stubCharacterCreatorService = null!;

        [SetUp]
        public void SetUp()
        {
            _mockAilmentRepository.Remove();
            _mockBuffRepository.Remove();
            _mockCharacterRepository.Remove();
            _mockOrderedItemRepository.Remove();
            _mockSlipRepository.Remove();
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
                characterRepository: _mockCharacterRepository);
            var stubSpeedService = new SpeedService(
                buffRepository: _mockBuffRepository,
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
                ailmentRepository: _mockAilmentRepository,
                ailmentPropertyFactory: stubAilmentPropertyFactory,
                battlePropertyFactory: _stubBattlePropertyFactory,
                bodyPartPropertyFactory: stubBodyPartPropertyFactory,
                bodyPartRepository: _mockBodyPartRepository,
                buffRepository: _mockBuffRepository,
                enhanceRepository: _mockEnhanceRepository,
                slipRepository: _mockSlipRepository);
            return stubCharacterCreatorService;
        }

        private OrderService GetOrderService()
        {
            var orderService = new OrderService(
                battlePropertyFactory: _stubBattlePropertyFactory,
                ailmentRepository: _mockAilmentRepository,
                characterRepository: _mockCharacterRepository,
                orderedItemRepository: _mockOrderedItemRepository,
                slipRepository: _mockSlipRepository,
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
                case CharacterEntity character:
                {
                    var orderedItem = new OrderedItemEntity(new OrderedItemId(), i);
                    orderedItem.SetOrderedItem(new ActorInTurn(character));
                    return orderedItem;
                }
                case AilmentCode ailmentCode:
                {
                    var orderedItem = new OrderedItemEntity(new OrderedItemId(), i);
                    orderedItem.SetOrderedItem(new ActorInTurn(ailmentCode));
                    return orderedItem;
                }
                case SlipCode slipCode:
                {
                    var orderedItem = new OrderedItemEntity(new OrderedItemId(), i);
                    orderedItem.SetOrderedItem(new ActorInTurn(slipCode));
                    return orderedItem;
                }
                default:
                    throw new InvalidCastException();
            }
        }

        private (CharacterEntity, string) AddPlayer()
        {
            var playerId = new CharacterId();
            var player = new CharacterEntity(
                id: playerId,
                characterTypeCode: CharacterTypeCode.Player,
                currentHitPoint: 179,
                currentTechnicalPoint: 50);
            _mockCharacterRepository.Add(player);
            _stubCharacterCreatorService.Create(playerId, isPlayer: true);
            return (player, "Player");
        }

        private (CharacterEntity, string) AddBee()
        {
            var beeId = new CharacterId();
            var bee = new CharacterEntity(
                id: beeId,
                characterTypeCode: CharacterTypeCode.Bee,
                currentHitPoint: 39,
                position: 0);
            _mockCharacterRepository.Add(bee);
            _stubCharacterCreatorService.Create(beeId);
            return (bee, "Bee");
        }

        private (CharacterEntity, string) AddBee2()
        {
            var beeId = new CharacterId();
            var bee = new CharacterEntity(
                id: beeId,
                characterTypeCode: CharacterTypeCode.Bee,
                currentHitPoint: 39,
                position: 0);
            _mockCharacterRepository.Add(bee);
            _stubCharacterCreatorService.Create(beeId);
            return (bee, "Bee");
        }

        private (CharacterEntity, string) AddSlime()
        {
            var slimeId = new CharacterId();
            var slime = new CharacterEntity(
                id: slimeId,
                characterTypeCode: CharacterTypeCode.Slime,
                currentHitPoint: 10,
                position: 1);
            _mockCharacterRepository.Add(slime);
            _stubCharacterCreatorService.Create(slimeId);
            return (slime, "Slime");
        }

        private (CharacterEntity, string) AddDragon()
        {
            var dragonId = new CharacterId();
            var dragon = new CharacterEntity(
                id: dragonId,
                characterTypeCode: CharacterTypeCode.Dragon,
                currentHitPoint: 56,
                position: 2);
            _mockCharacterRepository.Add(dragon);
            _stubCharacterCreatorService.Create(dragonId);
            return (dragon, "Dragon");
        }

        private (CharacterEntity, string) AddShuten()
        {
            var shutenId = new CharacterId();
            var shuten = new CharacterEntity(
                id: shutenId,
                characterTypeCode: CharacterTypeCode.Shuten,
                currentHitPoint: 56,
                position: 3);
            _mockCharacterRepository.Add(shuten);
            _stubCharacterCreatorService.Create(shutenId);
            return (shuten, "Shuten");
        }

        #endregion

        [Test]
        public void プレイヤーと敵2体の行動順()
        {
            var (player, playerName) = AddPlayer();
            var (bee, beeName) = AddBee();
            var (slime, slimeName) = AddSlime();

            _orderService.Update();
            var actual = _mockOrderedItemRepository.Get();

            var items = new object[]
            {
                player, bee, slime, player, bee, player, slime, bee, player, player, bee, player, slime, bee
            };
            var expected = GetExpectedValue(items);

            Assert.That(actual, Is.EqualTo(expected).Using(new OrderedItemEqualityComparator()));
        }

        [Test]
        public void プレイヤーと同種の敵2体の行動順()
        {
            var (player, playerName) = AddPlayer();
            var (bee, beeName) = AddBee();
            var (bee2, bee2Name) = AddBee2();

            _orderService.Update();
            var actual = _mockOrderedItemRepository.Get();

            var items = new object[]
            {
                player, bee, bee2, player, bee, bee2, player, bee, bee2, player, player, bee, bee2, player
            };
            var items2 = new object[]
            {
                player, bee2, bee, player, bee2, bee, player, bee2, bee, player, player, bee2, bee, player
            };
            var expected = GetExpectedValue(bee.Id.CompareTo(bee2.Id) < 0 ? items : items2);

            Assert.That(actual, Is.EqualTo(expected).Using(new OrderedItemEqualityComparator()));
        }

        [Test]
        public void プレイヤーと敵3体の行動順()
        {
            var (player, playerName) = AddPlayer();
            var (bee, beeName) = AddBee();
            var (slime, slimeName) = AddSlime();
            var (dragon, dragonName) = AddDragon();

            _orderService.Update();
            var actual = _mockOrderedItemRepository.Get();

            var items = new object[]
            {
                player, bee, slime, dragon, player, bee, player, slime, bee, player, dragon, player, bee, player
            };
            var expected = GetExpectedValue(items);

            Assert.That(actual, Is.EqualTo(expected).Using(new OrderedItemEqualityComparator()));
        }

        [Test]
        public void プレイヤーと敵4体の行動順()
        {
            var (player, playerName) = AddPlayer();
            var (bee, beeName) = AddBee();
            var (slime, slimeName) = AddSlime();
            var (dragon, dragonName) = AddDragon();
            var (shuten, shutenName) = AddShuten();

            _orderService.Update();
            var actual = _mockOrderedItemRepository.Get();

            var items = new object[]
            {
                player, bee, slime, dragon, shuten, player, bee, player, slime, bee, player, dragon, shuten, player
            };
            var expected = GetExpectedValue(items);

            Assert.That(actual, Is.EqualTo(expected).Using(new OrderedItemEqualityComparator()));
        }

        [Test]
        public void プレイヤーと敵4体とスリップダメージの行動順()
        {
            var (player, playerName) = AddPlayer();
            var (bee, beeName) = AddBee();
            var (slime, slimeName) = AddSlime();
            var (dragon, dragonName) = AddDragon();
            var (shuten, shutenName) = AddShuten();

            var poisoning = _mockSlipRepository.Get(SlipCode.Poisoning);
            poisoning.Effects = true;
            poisoning.AdvanceTurn();

            _orderService.Update();
            var actual = _mockOrderedItemRepository.Get();

            var items = new object[]
            {
                player, bee, slime, dragon, SlipCode.Poisoning, shuten, player, bee, player, SlipCode.Poisoning, slime,
                bee, player, dragon
            };
            var expected = GetExpectedValue(items);

            Assert.That(actual, Is.EqualTo(expected).Using(new OrderedItemEqualityComparator()));
        }

        private static void MockOrderedItemRepositoryLogger(
            IRepository<OrderedItemEntity, OrderedItemId> orderedItemRepository,
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