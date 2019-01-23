using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SavingCharlesRennieMacintosh
{
    class ScrollingLayer
    {
        public Texture2D image;
        public Vector2 scrollAmount;

        public float ParallaxRation = 0.5f;

        
        public void Draw(SpriteBatch batch, Camera gameCam)
        {
            // Get starting co-ordinate to draw tiles from
            Vector2 startPos = gameCam.cameraPos - new Vector2(gameCam.viewArea.Width / 2, gameCam.viewArea.Height / 2);

            // move start point to nearest valid background tile edge
            startPos.X =
                ((int)startPos.X / image.Width) * image.Width - image.Width;
            startPos.Y =
                ((int)startPos.Y / image.Height) * image.Height - image.Height * 2;

            // apply scrolling if necessary
            startPos.X += scrollAmount.X % image.Width - image.Width;
            startPos.Y += scrollAmount.Y % image.Height - image.Height;

            // set current tile column position to first column.
            Vector2 currentscreenpos = startPos;

            // get parallax scrolling offset
            Vector2 offset = new Vector2((gameCam.cameraPos.X * ParallaxRation) % image.Width, (gameCam.cameraPos.Y * ParallaxRation) % image.Height);

            do // fill horizontally
            {
                do // fill vertically
                {
                    batch.Draw(image, currentscreenpos + offset, Color.White);
                    currentscreenpos.Y += image.Height;
                } while (currentscreenpos.Y - startPos.Y < gameCam.viewArea.Height + image.Height * 4);
                currentscreenpos.X += image.Width;  // move on to next column
                currentscreenpos.Y = startPos.Y;    // reset to top of tile column
            } while (currentscreenpos.X - startPos.X < gameCam.viewArea.Width + image.Width * 5);
        }

        public void Draw(SpriteBatch batch, int width, int height)
        {

            scrollAmount.X = scrollAmount.X % image.Width - image.Width;
            scrollAmount.Y = scrollAmount.Y % image.Height - image.Height;
            Vector2 currentScreenPos = scrollAmount;

            do
            {
                do
                {
                    batch.Draw(image, currentScreenPos, Color.White);
                    currentScreenPos.Y += image.Height;

                } while (currentScreenPos.Y < height);

                currentScreenPos.Y = scrollAmount.Y;
                currentScreenPos.X += image.Width;
            } while (currentScreenPos.X < width);


        }
    }
}
