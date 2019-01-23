using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SavingCharlesRennieMacintosh
{
    class Enemy : GameSprite
    {
        public int enemyHealth = 2;

        public void Initialize(int x, int y, ContentManager content)
        {
           // AddAnimation(content.Load<Texture2D>("enemyTest1"), 7, 500);
            

            position = new Vector2(x, y);
        }

        public override void Update(int milliseconds)
        {
            base.Update(milliseconds);

            //faces different ways when moving 
            if (velocity.X == 1) flipHorizontally = true;
            if (velocity.X == -1) flipHorizontally = false;

            //faces different ways when attacking 
            if (velocity.X == 5) flipHorizontally = true;
            if (velocity.X == -5) flipHorizontally = false;
        }
    }
}
