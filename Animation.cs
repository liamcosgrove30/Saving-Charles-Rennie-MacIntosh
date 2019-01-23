using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SavingCharlesRennieMacintosh
{
    class Animation 
    {
        public Texture2D sheet;
        Rectangle[] frameRectangles;
        public int numberOfFrames;
        int Timer;
        int totalAnimationLength;
        public int currentAnimationFrame;
        int currentAnimationTimer;


        public Animation(Texture2D spriteSheet,
            int frames, int length)
        {
            sheet = spriteSheet;
            numberOfFrames = frames;
            totalAnimationLength = length;
            int pixelsPerFrame = spriteSheet.Width / frames;
            frameRectangles = new Rectangle[numberOfFrames];
            for (int index = 0; index < numberOfFrames; index++)
            {
                frameRectangles[index] = new Rectangle(index * pixelsPerFrame, 0, pixelsPerFrame, spriteSheet.Height);
            }
        }

        public void Update(int milliseconds)
        {
            if (totalAnimationLength <= 0) return;
            currentAnimationTimer += milliseconds;
            while (currentAnimationTimer >= totalAnimationLength)
                currentAnimationTimer -= totalAnimationLength;
            currentAnimationFrame =
                (numberOfFrames * currentAnimationTimer) /
                totalAnimationLength;
        }

        public void Draw(SpriteBatch batch, Vector2 position)
        {
            batch.Draw(sheet, position,
                frameRectangles[currentAnimationFrame], Color.White);
        }

        public void Draw(SpriteBatch batch, Vector2 position, SpriteEffects flipping)
        {
            batch.Draw(sheet, position,
                frameRectangles[currentAnimationFrame], Color.White,
                0, Vector2.Zero, 1, flipping, 0);
        }
    }
}
