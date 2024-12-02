using System.Collections.Generic;
using BattleScene.DataAccess.Dto;
using BattleScene.DataAccess.Factory;
using BattleScene.DataAccess.Resource;
using BattleScene.DataAccess.ScriptableObjects;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.Service.Order;
using NSubstitute;
using NUnit.Framework;
using Tests.BattleScene.DataAccess.Repository;
using Tests.BattleScene.DataAccess.Resource;
using UnityEditor;
using UnityEngine;
// ReSharper disable UnusedMember.Local

namespace Tests.BattleScene.UseCases.Service
{
    // TODO: wip
    [TestFixture]
    public class OrderServiceTest
    {
        private BattlePropertyFactory _stubBattlePropertyFactory;
        private CharacterPropertyFactory _stubCharacterPropertyFactory;
        private CharacterPropertyFactoryService _stubCharacterPropertyFactoryService = null!;
        private readonly MockCollection<BuffEntity, (CharacterId, BuffCode)> _mockBuffCollection = new();
        private readonly MockCollection<CharacterEntity, CharacterId> _mockCharacterCollection = new();
        private readonly MockCollection<AilmentEntity, (CharacterId, AilmentCode)> _ailmentCollection = new();
        private readonly MockCollection<OrderedItemEntity, OrderId> _orderedItemCollection = new();
        private readonly MockCollection<SlipEntity, SlipCode> _slipDamageCollection = new();
        private ISpeedService _mockSpeedService = null!;

        [SetUp]
        public void SetUp()
        {
            var stubBattlePropertyResource = Object.FindFirstObjectByType<BattlePropertyResource>();
            _stubBattlePropertyFactory = new BattlePropertyFactory(
                battlePropertyResource: stubBattlePropertyResource);

            var characterPropertyScriptableObject
                = AssetDatabase.LoadAssetAtPath<BaseScriptableObject<CharacterPropertyDto, CharacterTypeCode>>(
                    "Assets/ScriptableObject/CharacterPropertyScriptableObject.asset");
            var stubCharacterPropertyResource
                = new StubBaseScriptableObjectResource<CharacterPropertyDto, CharacterTypeCode>(
                    itemList: characterPropertyScriptableObject.ItemList);
            _stubCharacterPropertyFactory = new CharacterPropertyFactory(
                propertyResource: stubCharacterPropertyResource);

            _stubCharacterPropertyFactoryService = new CharacterPropertyFactoryService(
                characterPropertyFactory: _stubCharacterPropertyFactory,
                characterCollection: _mockCharacterCollection);

            _mockSpeedService = new SpeedService(
                buffCollection: _mockBuffCollection,
                characterPropertyFactory: _stubCharacterPropertyFactoryService);
        }

        [Test]
        public void スリップダメージが5の倍数順に挿入される()
        {
            var stubAilmentRepository = Substitute.For<ICollection<AilmentEntity, (CharacterId, AilmentCode)>>();
            stubAilmentRepository.Get().Returns(new List<AilmentEntity>());

            var playerId = new CharacterId();
            var player = new CharacterEntity(
                id: playerId,
                characterTypeCode: CharacterTypeCode.Player,
                currentHitPoint: 179,
                currentTechnicalPoint: 50);
            var beeId = new CharacterId();
            var bee = new CharacterEntity(
                id: beeId,
                characterTypeCode: CharacterTypeCode.Bee,
                currentHitPoint: 39,
                position: 0);

            _mockCharacterCollection.Add(player);
            _mockCharacterCollection.Add(bee);

            var mockOrderedItemRepository = new MockCollection<OrderedItemEntity, OrderId>();

            var stubSlipDamageRepository = Substitute.For<ICollection<SlipEntity, SlipCode>>();
            stubSlipDamageRepository.Get().Returns(new List<SlipEntity>()
                { new SlipEntity(SlipCode.Poisoning, true, 5) });

            var orderService = new OrderService(
                battlePropertyFactory: _stubBattlePropertyFactory,
                characterPropertyFactory: _stubCharacterPropertyFactoryService,
                ailmentCollection: stubAilmentRepository,
                characterCollection: _mockCharacterCollection,
                orderedItemCollection: mockOrderedItemRepository,
                slipDamageCollection: stubSlipDamageRepository,
                speed: _mockSpeedService);

            orderService.Update();
            var orderedItemRepositoryToString = mockOrderedItemRepository.ToString();
            Debug.Log(orderedItemRepositoryToString);

            var orderedItemList = mockOrderedItemRepository.Get();

            orderedItemList[0].TryGetCharacterId(out var characterId1);
            Assert.That(characterId1, Is.EqualTo(playerId), "順番の1番目は正しい");
            orderedItemList[1].TryGetCharacterId(out var characterId2);
            Assert.That(characterId2, Is.EqualTo(beeId), "順番の2番目は正しい");
        }
    }
}