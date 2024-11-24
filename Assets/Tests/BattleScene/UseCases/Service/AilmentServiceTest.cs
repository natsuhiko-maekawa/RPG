using BattleScene.DataAccess.Dto;
using BattleScene.DataAccess.Factory;
using BattleScene.DataAccess.ScriptableObjects;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;
using BattleScene.UseCases.Service;
using NUnit.Framework;
using Tests.BattleScene.DataAccess.Repository;
using Tests.BattleScene.DataAccess.Resource;
using UnityEditor;

namespace Tests.BattleScene.UseCases.Service
{
    [TestFixture]
    public class AilmentServiceTest
    {
        private IActualTargetIdPickerService _mockActualTargetIdPickerService;
        private IFactory<AilmentPropertyValueObject, AilmentCode> _stubAilmentPropertyFactory;
        private readonly MockCollection<AilmentEntity, (CharacterId, AilmentCode)> _mockAilmentCollection = new();
        
        [SetUp]
        public void SetUp()
        {
            _mockActualTargetIdPickerService = new MockActualTargetIdPickerService();
            var ailmentPropertyScriptableObject
                = AssetDatabase.LoadAssetAtPath<BaseScriptableObject<AilmentPropertyDto, AilmentCode>>(
                    "Assets/ScriptableObject/AilmentPropertyScriptableObject.asset");
            var stubAilmentPropertyResource
                = new StubBaseScriptableObjectResource<AilmentPropertyDto, AilmentCode>(
                    ailmentPropertyScriptableObject.ItemList);
            _stubAilmentPropertyFactory = new AilmentPropertyFactory(stubAilmentPropertyResource);
        }

        [Test]
        public void Test1()
        {
            var ailmentService = new AilmentService(
                actualTargetIdPicker: _mockActualTargetIdPickerService,
                ailmentPropertyFactory: _stubAilmentPropertyFactory,
                ailmentCollection: _mockAilmentCollection);
        }
    }
}