﻿namespace BattleScene.Framework.ViewModel
{
    public struct Row
    {
        public string RowName { get; }
        public string[] RowDescription { get; }
        public string PlayerImagePath { get; }
        public bool Enabled { get; }
        public int TechnicalPoint { get; }

        public Row(
            string rowName, 
            string[] rowDescription, 
            string playerImagePath, 
            bool enabled, 
            int technicalPoint)
        {
            RowName = rowName;
            RowDescription = rowDescription;
            PlayerImagePath = playerImagePath;
            Enabled = enabled;
            TechnicalPoint = technicalPoint;
        }
    }
}