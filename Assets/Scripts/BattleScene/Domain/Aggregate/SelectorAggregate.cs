﻿using BattleScene.Domain.Entity;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.Aggregate
{
    public class SelectorAggregate
    {
        private readonly SelectorEntity _selectorEntity;

        public SelectorAggregate(
            SelectorId selectorId,
            int maxViewLength,
            int listLength)
        {
            SelectorId = selectorId;
            _selectorEntity = new SelectorEntity(maxViewLength, listLength);
        }

        public SelectorId SelectorId { get; }

        public void Up()
        {
            _selectorEntity.Up();
        }

        public void Down()
        {
            _selectorEntity.Down();
        }

        public void Left()
        {
            _selectorEntity.Left();
        }

        public void Right()
        {
            _selectorEntity.Right();
        }

        public int GetSelection()
        {
            return _selectorEntity.Selection;
        }

        public SelectorEntity GetSelector()
        {
            return _selectorEntity;
        }
    }
}