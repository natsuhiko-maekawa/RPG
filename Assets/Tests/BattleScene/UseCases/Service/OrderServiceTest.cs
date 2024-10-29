using System.Collections.Generic;
using BattleScene.DataAccess.Dto;
using BattleScene.DataAccess.Factory;
using BattleScene.DataAccess.Resource;
using BattleScene.DataAccess.ScriptableObjects;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.Service.Order;
using NSubstitute;
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
        private BattlePropertyFactory _stubBattlePropertyFactory;
        private CharacterPropertyFactory _stubCharacterPropertyFactory;
        private CharacterPropertyFactoryService _stubCharacterPropertyFactoryService = null!;
        private readonly MockCollection<BuffEntity, (CharacterId, BuffCode)> _mockBuffCollection = new();
        private readonly MockCollection<CharacterEntity, CharacterId> _mockCharacterCollection = new();
        
        [SetUp]
        public void SetUp()
        {
            var stubBattlePropertyResource = Object.FindObjectOfType<BattlePropertyResource>();
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

            _stubCharacterPropertyFactoryService = new CharacterPropertyFactoryService(
                characterPropertyFactory: _stubCharacterPropertyFactory,
                characterCollection: _mockCharacterCollection);
            
            var mockSpeedService = new SpeedService(
                buffCollection: _mockBuffCollection,
                characterPropertyFactory: _stubCharacterPropertyFactoryService);
            
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
                speed: mockSpeedService);
            
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