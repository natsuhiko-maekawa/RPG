using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Aggregate
{
    public class SkillSelectorAggregate
    {
        private readonly SelectorEntity _selectorEntity;

        public SkillSelectorAggregate(
            SkillSelectorId skillSelectorId,
            int maxViewLength,
            int listLength)
        {
            SkillSelectorId = skillSelectorId;
            _selectorEntity = new SelectorEntity(maxViewLength, listLength);
        }

        public SkillSelectorId SkillSelectorId { get; }

        public void Up()
        {
            _selectorEntity.Up();
        }

        public void Down()
        {
            _selectorEntity.Down();
        }

        public SkillCode GetSkill(IList<SkillCode> skillEnumList)
        {
            return skillEnumList[_selectorEntity.ListStart + _selectorEntity.Selection];
        }

        public IList<SkillCode> GetSkillList(IList<SkillCode> skillEnumList)
        {
            return skillEnumList
                .Where((_, i) =>
                    _selectorEntity.ListStart <= i && i < _selectorEntity.ListStart + _selectorEntity.ActualViewLength)
                .ToList();
        }

        public SelectorEntity GetSelector()
        {
            return _selectorEntity;
        }
    }
}