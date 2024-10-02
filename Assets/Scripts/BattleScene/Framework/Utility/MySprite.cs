using UnityEngine;

namespace BattleScene.Framework.Utility
{
    public static class MySprite
    {
        public static Sprite[] CreateByGrid(Texture2D texture, int col, int row)
        {
            var spriteArray = new Sprite[col * row];
            var width = texture.width / (float)col;
            var height = texture.height / (float)row;

            for (var r = 0; r < col; ++r)
            for (var c = 0; c < row; ++c)
            {
                var rect = new Rect(c * width, r * height, width, height);
                spriteArray[c + (row - 1 - r) * row] = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
            }

            return spriteArray;
        }
    }
}